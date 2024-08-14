using UnityEngine;
using SQLite4Unity3d;
using System.IO;

public class DatabaseManager : MonoBehaviour {
    private SQLiteConnection _connection;

    void Start() {

        string file = ConfigManager.Get<string>("Database.Connection.File");

        Debug.Log("config: " + file);



        string dbPath = Path.Combine(Application.persistentDataPath, "GameData.db");
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        // テーブルの作成
        _connection.CreateTable<PlayerData>();
        _connection.CreateTable<GameData>();

        // サンプルデータの挿入
        _connection.InsertAll(new[]{
            new PlayerData { Id = 1, Name = "Player1" },
            new PlayerData { Id = 2, Name = "Player2" }
        });

        _connection.InsertAll(new[]{
            new GameData { PlayerId = 1, Score = 100 },
            new GameData { PlayerId = 2, Score = 200 },
            new GameData { PlayerId = 1, Score = 150 }
        });

        // データの結合とフィルタリング
        var query = _connection.Query<JoinedData>(
            "SELECT PlayerData.Name, GameData.Score " +
            "FROM PlayerData " +
            "INNER JOIN GameData ON PlayerData.Id = GameData.PlayerId " +
            "WHERE GameData.Score > ?",
            100);

        foreach (var data in query) {
            Debug.Log($"Name: {data.Name}, Score: {data.Score}");
        }
    }

    public class PlayerData {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GameData {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int Score { get; set; }
    }

    public class JoinedData {
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
