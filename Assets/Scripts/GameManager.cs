using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;// Singleton instance
    public event EventHandler OnSpawnObstacle;
    public event EventHandler OnSpawnEnemy; // Event for spawning enemies
    public event EventHandler OnPickupsActivated;
    LevelGenerate levelGenerate; // Reference to LevelGenerate script
    public int obstacleScore = 0;

    private void Awake()
    {
        Instance = this; // So other scripts can access GameManager.Instance
        levelGenerate = FindAnyObjectByType<LevelGenerate>();
        if (levelGenerate == null)
        {
            Debug.LogWarning("LevelGenerate not found in scene!");
        }
    }
    void OnEnable()
    {
        GroundTile.OnBossPassed += HandleBossPassed;
        Enemy.OnEnemyBeaten += HandleEnemyBeaten; // Subscribe to the OnEnemyDeath event
    }

    void OnDisable()
    {
        GroundTile.OnBossPassed -= HandleBossPassed;
        Enemy.OnEnemyBeaten -= HandleEnemyBeaten; // Unsubscribe from the OnEnemyPassed event
    }
    private void Update()
    {
        // Check spawn an obstacle
        if (obstacleScore == 0 && OnSpawnObstacle != null)
        {
            OnSpawnObstacle(this, EventArgs.Empty);// Trigger the OnSpawnObstacle event
        }
    }
    public void LogPickupActivated(string pickupType)
    {
        Debug.Log("Pickup activated: " + pickupType);// Log the pickup type to the console
    }
    void HandleBossPassed(int score)
    {
        Debug.Log("Boss Passed " + score);
    }
    void HandleEnemyBeaten(int score)
    {
        Debug.Log("HandleEnemyBeaten called with score: " + score);
        if (levelGenerate != null)
        {
            levelGenerate.beatenScore++;
        }
        Debug.Log("Boss Beaten Score: " + score + "\nLevels beaten are: " + (levelGenerate != null ? levelGenerate.beatenScore : 0));

    }
}
