using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

[System.Serializable]
public class PlayerStatsRecord
{
    [BsonIgnore] // This field is ignored by MongoDB serialization
    public ObjectId _id { get; set; }

    public string playerName { get; set; }
    public int score { get; set; }
}