using UnityEngine;
using SQLite;
using System.IO;
using System;

public class DatabaseManager : MonoBehaviour {
    private const string ConfigDatabaseFile = "Database.Connection.File";
    private const string ConfigMigrationPath = "Database.Migration.Path";
    private SQLiteConnection _connection;

    private void Start() {
        CreateConnection();
        Migrate();
    }

    private void OnDestroy() {
        _connection?.Close();
    }

    private void OnApplicationQuit() {
        _connection?.Close();
    }

    private void CreateConnection() {
        string file = ConfigManager.Get<string>(ConfigDatabaseFile);
        Debug.Log($"Load database file: {file}");

        string dbPath = Path.Combine(Application.persistentDataPath, file);
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
    }

    private void Migrate() {
        SetupMigration();

        string migrationPath = ConfigManager.Get<string>(ConfigMigrationPath);
        Debug.Log($"Load migration path: {migrationPath}");

        if (Directory.Exists(migrationPath)) {
            string[] migrationFiles = Directory.GetFiles(migrationPath, "*.sql");
            Array.Sort(migrationFiles);

            foreach (var file in migrationFiles) {
                string sql = File.ReadAllText(file);

                // Skip the file if it is empty
                if (string.IsNullOrWhiteSpace(sql)) {
                    Debug.LogWarning($"Skipped empty migration file: {file}");
                    continue;
                }

                string migrationName = Path.GetFileNameWithoutExtension(file);

                // if migrationName is already executed, skip it
                string checkMigrationQuery = $"SELECT COUNT(*) FROM migrations WHERE name = '{migrationName}'";
                if (_connection.ExecuteScalar<int>(checkMigrationQuery) > 0) {
                    Debug.Log($"Skipped already executed migration: {migrationName}");
                    continue;
                }

                // Execute the migration
                Debug.Log($"Execute migration: {migrationName}");
                _connection.Execute(sql);

                // Record the migration
                string recordMigrationQuery = $"INSERT INTO migrations (name) VALUES ('{migrationName}')";
                _connection.Execute(recordMigrationQuery);
            }
        }
    }

    private void SetupMigration() {
        string createMigrationsTableQuery = @"
            CREATE TABLE IF NOT EXISTS migrations (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT NOT NULL,
                executed_at DATETIME DEFAULT CURRENT_TIMESTAMP
            );
        ";

        _connection.Execute(createMigrationsTableQuery);
    }

    public SQLiteConnection GetConnection() {
        return _connection;
    }
}
