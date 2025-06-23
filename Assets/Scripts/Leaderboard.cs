using UnityEngine;
using System.Collections.Generic;
public class Leaderboard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Text for lesderboard
    [SerializeField] TMPro.TextMeshProUGUI leaderboardText;
    List<PlayerStatsRecord> playerStatsRecords = new List<PlayerStatsRecord>();
    void Start()
    {

        // fetch player stats records from the PlayerStatsManager
        playerStatsRecords = PlayerStatsManager.Instance.GetLeaderBoard(10);

        // Initialize the leaderboard text
        leaderboardText.text = "Leaderboard:\n\n";
        leaderboardText .text += "Top 10 Players:\n\n";

        // Example data, replace with actual leaderboard data retrieval logic
        for (int i = 0; i < playerStatsRecords.Count; i++)
        {
            PlayerStatsRecord record = playerStatsRecords[i];
            leaderboardText.text += $"{i + 1}. {record.playerName} - Score: {record.score}\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
