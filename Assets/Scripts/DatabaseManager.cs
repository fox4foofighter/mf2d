using UnityEngine;
using SQLite4Unity3d;
using System.IO;

public class DatabaseManager : MonoBehaviour {
    private const string ConfigDatabaseFile = "Database.Connection.File";
    private const string ConfigMigrationPath = "Database.Migration.Path";
    private SQLiteConnection _connection;


    private void Start() {
        CreateConnection();

        Migrate();

    }

    private void OnDestroy() {
            _connection.Close();
    }

    private void OnApplicationQuit() {
        _connection.Close();
    }


    private void CreateConnection() {
        string file = ConfigManager.Get<string>(ConfigDatabaseFile);
        Debug.Log($"Load database file: {file}");

        string dbPath = Path.Combine(Application.persistentDataPath, file);
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
    }

    private void Migrate() {
        string migrationPath = ConfigManager.Get<string>(ConfigMigrationPath);
        Debug.Log($"Load migration path: {migrationPath}");

        if (Directory.Exists(migrationPath)) {
            string[] migrationFiles = Directory.GetFiles(migrationPath, "*.sql");

            foreach (var file in migrationFiles) {
                Debug.Log($"Execute migration file: {file}");
                string sql = File.ReadAllText(file);
                _connection.Execute(sql);
            }
        }
    }
}
