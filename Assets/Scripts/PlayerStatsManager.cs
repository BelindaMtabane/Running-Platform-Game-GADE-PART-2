using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance { get; private set; }
    private const string connectionString = "mongodb+srv://gadepoeuser:gadepoeuser@harrietcluster.p1vtdry.mongodb.net/?retryWrites=true&w=majority&appName=HarrietCluster"; // Update with your MongoDB connection string
    private const string databaseName = "gadepoe"; // Update with your database name
    private MongoClient client;
    private IMongoDatabase database;

    private void Awake()
    {
        client = new MongoClient(connectionString);
        database = client.GetDatabase(databaseName);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this instance across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    // Add a game record to the database

    public void AddGame(PlayerStatsRecord record)
    {
        try
        {
            var collection = database.GetCollection<PlayerStatsRecord>("PlayerStats");
            collection.InsertOne(record);
            Debug.Log("PlayerStatsRecord inserted successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to insert PlayerStatsRecord: " + ex.Message);
        }
    }

    public List<PlayerStatsRecord> GetAllGames()
    {
        var collection = database.GetCollection<PlayerStatsRecord>("PlayerStats");
        return collection.Find(_ => true).ToList();
    }

    public List<PlayerStatsRecord> getPlayerGames(string playerName)
    {
        var collection = database.GetCollection<PlayerStatsRecord>("PlayerStats");
        return collection.Find(record => record.playerName == playerName).ToList();
    }

    public PlayerStatsRecord GetTopScore(string playerName)
    {
        var collection = database.GetCollection<PlayerStatsRecord>("PlayerStats");
        return collection.Find(record => record.playerName == playerName)
                        .SortByDescending(record => record.score)
                        .FirstOrDefault();
    }

    // get leader board of the top 10 players by score
    public List<PlayerStatsRecord> GetLeaderBoard(int topN = 10)
    {
        var collection = database.GetCollection<PlayerStatsRecord>("PlayerStats");
        var pipeline = new[]
        {
            new BsonDocument("$sort", new BsonDocument("score", -1)),
            new BsonDocument("$group", new BsonDocument
            {
                { "_id", "$playerName" },
                { "playerName", new BsonDocument("$first", "$playerName") },
                { "score", new BsonDocument("$first", "$score") }
            }),
            new BsonDocument("$sort", new BsonDocument("score", -1)),
            new BsonDocument("$limit", topN),
            new BsonDocument("$project", new BsonDocument
            {
                { "_id", 0 },
                { "playerName", 1 },
                { "score", 1 }
            })
        };

        try
        {
            var results = collection.Aggregate<PlayerStatsRecord>(pipeline).ToList();
            Debug.Log($"GetLeaderBoard: Retrieved {results.Count} records.");
            foreach (var record in results)
            {
                Debug.Log($"Leaderboard Entry: {record.playerName} - Score: {record.score}");
            }
            return results;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("GetLeaderBoard failed: " + ex.Message);
            return new List<PlayerStatsRecord>();
        }
    }

}