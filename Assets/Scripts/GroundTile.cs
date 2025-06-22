using System;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner; // Reference to the GroundSpawner script
    public GameManager gameManager;
    //public GameObject rockPrefab;
    [SerializeField] GameObject obstaclePrefab; // Prefab for the obstacle
    //[SerializeField] private Obstacle obstacleScript; // Assign this in the Inspector
    // Position to spawn the obstacle
                               // [SerializeField] GameObject coinPrefab; // Prefab for the coin, {HealthCoin, SheinCoin, SpeedUpCoin, PointsCoin}
    public GameObject[] pickupPrefab;// Prefab for the pick up
    private Collider groundCollider;
    public GameObject healthPrefab;
    public GameObject coalPrefab;
    public GameObject pointsPrefab;
    public GameObject timeorbPrefab;

    public GameObject HandPrefab;
    public GameObject SpikePrefab;
    public GameObject portalPrefab;
    Vector3 obstaclePosition;
    public GameObject enemyPrefab; // to Assign in GroundSpawner when spawning
    public int tileIndex; // Sets in GroundSpawner when spawning
    List<Vector3> usedPositions = new List<Vector3>();
    float minDistance = 4f; // Minimum distance between spawned objects

    public static int obstacleScore = 0; // Static variable to keep track of the number of obstacles passed

    void Start()
    {
       
        groundSpawner = GameObject.FindAnyObjectByType<GroundSpawner>(); // Find the GroundSpawner script in the scene
        groundCollider = GetComponent<Collider>();
        GameManager gameManager = GameObject.FindAnyObjectByType<GameManager>(); // Find the GameManager script in the scene
        if (gameManager != null)
        {
            gameManager.OnSpawnObstacle += HandleSpawnObstacle;
        }

        SpawnEnemy(); // Call the SpawnEnemy
        SpawnPickUps(); // Call the SpawnCoins method to spawn coins
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }

        groundSpawner.SpawnTile(); // Call the SpawnTile method in the GroundSpawner script when the player exits the trigger
        Destroy(gameObject, 60);
    }
    /*private void OnCollisionExit(Collision collision)
    {
        groundSpawner.SpawnTile(); // Call the SpawnTile method in the GroundSpawner script when the player exits the trigger
        Destroy(gameObject, 120);
    }*/

    // Update is called once per frame
    void Update()
    {

    }

    private void HandleSpawnObstacle(object sender, EventArgs e)
    {
        int obstacleToSpawn = 4; // Number of pickups to spawn
        GameObject[] obstacles = { HandPrefab, SpikePrefab, portalPrefab }; // Array of obstacle prefabs

        for (int i = 0; i < obstacleToSpawn; i++)
        {
            int randomSpawnIndex = UnityEngine.Random.Range(0, obstacles.Length); // Randomly select a pickup prefab from the array
            GameObject obstacle = Instantiate(obstacles[randomSpawnIndex]);

            Vector3 spawnPos = GetSpacedPoint(groundCollider);
            obstacle.transform.position = spawnPos;
            GameManager.Instance.obstacleScore++;
            Debug.Log("Obstacle Passed. Score: " + GameManager.Instance.obstacleScore);

            Destroy(obstacle, 60); // Destroy after 60 seconds
        }
    }

    void SpawnEnemy()
    {

        //if (tileIndex < 1) return; // Don't spawn enemies on the first tile
        GameObject enObj = Instantiate(enemyPrefab);

        // try and get center if available
        Vector3 enObjPos = GetCenterPointCollider(groundCollider);

        if (Vector3.Distance(enObjPos, obstaclePosition) < 1.0f)
        {
            enObjPos = GetRandomPointCollider(groundCollider);
            while (Vector3.Distance(enObjPos, obstaclePosition) < 1.0f)
            {
                enObjPos = GetRandomPointCollider(groundCollider);// Check if the coin position is the same as the obstacle position
            }
        }
        
        enObj.transform.position = new Vector3(enObjPos.x, enObjPos.y -0.3f, enObjPos.z);
        // Destroy(enObj, 20);
    }

    void SpawnPickUps()
    {
        int pickupsToSpawn = 6; // Number of pickups to spawn
        GameObject[] pickups = { healthPrefab, coalPrefab, pointsPrefab, timeorbPrefab }; // Array of pickup prefabs

        for (int i = 0; i < pickupsToSpawn; i++)
        {
            int selected = UnityEngine.Random.Range(0, pickups.Length);
            GameObject pickup = Instantiate(pickups[selected]);

            Vector3 spawnPos = GetSpacedPoint(groundCollider);
            pickup.transform.position = spawnPos;
            Destroy(pickup, 60);
        }
    }
    Vector3 GetRandomPointCollider(Collider collider)
    {
        Vector3 randomPoint = new Vector3(
            UnityEngine.Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            UnityEngine.Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            UnityEngine.Random.Range(collider.bounds.min.z, collider.bounds.max.z)
        ); // Generate a random point within the bounds of the collider
        if (randomPoint != collider.ClosestPoint(randomPoint))
        {
            randomPoint = GetRandomPointCollider(collider); // Recursively call the method until a valid point is found
        }
        randomPoint.y = 1; // Set the Y coordinate to 1, matching it to the ground level
        return randomPoint;
    }
    //ycyicuiviohbopnopbpiboipbopon
    Vector3 GetCenterPointCollider(Collider collider)
    {
        Vector3 center = collider.bounds.center;
        center.y = 1f;// Set the Y coordinate to 1, matching it to the ground level
        return center;
    }
    Vector3 GetSpacedPoint(Collider collider)
    {
        Vector3 point = GetRandomPointCollider(collider);// Generate a random point within the collider bounds
        int attempts = 0;// Counter for attempts to find a valid point

        while (!IsFarEnough(point) && attempts < 20)
        {
            point = GetRandomPointCollider(collider);// Generate a new random point within the collider bounds
            Debug.Log("Collider bounds size: " + groundCollider.bounds.size);// Log the size of the collider bounds
            attempts++;// Increment the attempts counter
        }

        usedPositions.Add(point);// Add the point to the list of used positions
        return point;
    }

    bool IsFarEnough(Vector3 point)
    {
        foreach (Vector3 used in usedPositions)
        {
            float xDiff = Mathf.Abs(point.x - used.x);// Calculate the absolute differences in X coordinates
            float zDiff = Mathf.Abs(point.z - used.z);// Calculate the absolute differences in X and Z coordinates

            if (xDiff < minDistance || zDiff < minDistance)
            {
                
                return false; // Too close in both X and Z
            }
        }
        Debug.Log("Point is far enough from all used positions.");

        return true;// Point is far enough from all used positions
    }

}

/* public static int tileCounter = 0; // Static variable to keep track of the number of tiles spawned
 * public gameObject enemyPrefab; // Prefab for the enemy
 * public transform player; // Reference to the player transform
 * 
 * void start()
 * {
 * 
 *       tile counter++; // Increment the tile counter when a new tile is spawned
 *           if (tileCounter == 6) // Check if this is the 6th tile
 *           {
 *                   spawn enemy(); // Call the method to spawn the enemy
 *           }
 * }
 * void spawn enemy()
 * {
 * 
 *      Vector3 spawn position = player.position + player.forward * 2.0f; // Spawn the enemy in front of the player
 * 
 *      gameObject enemy = instantiate(enemyPrefab, spawnPosition, quaternion.identity); // Instantiate the enemy prefab
 *      
 *      vector3  lookAt = player.position; // Set the enemy's look-at position to the player's position
 *      lookAt.y = enemy.transform.position.y; // Keep the Y coordinate the same
 *      enemy.transform.LookAt(lookAt); // Make the enemy look at the player
 * }
 * 
 * 
 * */