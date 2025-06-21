using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;// Singleton instance
    public event EventHandler OnSpawnObstacle;
    public event EventHandler OnPickupsActivated;
    public int obstacleScore = 0;

    private void Awake()
    {
        Instance = this; // So other scripts can access GameManager.Instance
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
}
