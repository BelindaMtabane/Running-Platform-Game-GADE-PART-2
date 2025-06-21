using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance;
    public List<PlayerStatsRecord> allGames = new List<PlayerStatsRecord>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadStats();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGame(PlayerStatsRecord record)
    {
        allGames.Add(record);
        SaveStats();
    }

    public PlayerStatsRecord GetTopScore()
    {
        PlayerStatsRecord top = null;
        foreach (var rec in allGames)
        {
            if (top == null || rec.points > top.points)
                top = rec;
        }
        return top;
    }

    private void SaveStats()
    {
        string json = JsonUtility.ToJson(new Wrapper { games = allGames });
        PlayerPrefs.SetString("PlayerStatsHistory", json);
        PlayerPrefs.Save();
    }

    private void LoadStats()
    {
        string json = PlayerPrefs.GetString("PlayerStatsHistory", "");
        if (!string.IsNullOrEmpty(json))
        {
            allGames = JsonUtility.FromJson<Wrapper>(json).games;
        }
    }

    [System.Serializable]
    private class Wrapper
    {
        public List<PlayerStatsRecord> games;
    }
}