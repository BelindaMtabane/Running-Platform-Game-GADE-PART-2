using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner; // Reference to the GroundSpawner script
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

    void Start()
    {
        groundSpawner = GameObject.FindAnyObjectByType<GroundSpawner>(); // Find the GroundSpawner script in the scene
        groundCollider = GetComponent<Collider>();
        
        SpawnObstacle(); // Call the SpawnObstacle method to spawn an obstacle
        SpawnEnemy(); // Call the SpawnEnemy
        SpawnPickUps(); // Call the SpawnCoins method to spawn coins
    }

    private void OnTriggerExit(Collider other)
    {
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

    void SpawnObstacle()
    {
        /*
         * 
         * GameObject[] obstacles = { HandPrefab, SpikePrefab, portalPrefab};
         * int randomSpawnIndex = Random.Range(2, 5); // Randomly select a spawn index for the obstacle
        Transform spawnPoint = transform.GetChild(randomSpawnIndex).transform; // Get the spawn point from the child of the ground tile
        // Randomly choose an ObstacleType from the enum
        //Obstacle.ObstacleTypes randomObstacleType = (Obstacle.ObstacleTypes)Random.Range(0, System.Enum.GetValues(typeof(Obstacle.ObstacleTypes)).Length);

        // Instantiate the prefab directly using the selected ObstacleType
        //GameObject obstaclePrefab = obstacleScript.obstacleTypes[(int)randomObstacleType];
        //GameObject obstaclePrefabs = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform); // Instantiate the obstacle prefab at the spawn point
        GameObject temp = Instantiate(obstacles[selectedObstacle]);
        obstaclePositions = spawnPoint.position;/* // Set the obstacle position to the spawn point position*/
        /*GameObject[] obstacles = { HandPrefab, SpikePrefab, portalPrefab };
        int randomSpawnIndex = Random.Range(2, 5); // Randomly select a spawn index for the obstacle
        Transform spawnPoint = transform.GetChild(randomSpawnIndex).transform; // Get the spawn point from the child of the ground tile
        GameObject temp = Instantiate(obstacles[randomSpawnIndex]);
        obstaclePositions = spawnPoint.position; // Set the obstacle position to the spawn point position*/
        int obstacleToSpawn = 4; // Number of pickups to spawn
        GameObject[] obstacles = { HandPrefab, SpikePrefab, portalPrefab }; // Array of obstacle prefabs

        for (int i = 0; i < obstacleToSpawn; i++)
        {
            int randomSpawnIndex = Random.Range(0, obstacles.Length); // Randomly select a pickup prefab from the array
            GameObject obstacle = Instantiate(obstacles[randomSpawnIndex]);

            Vector3 spawnPos = GetSpacedPoint(groundCollider);
            obstacle.transform.position = spawnPos;
           /* obstacle.transform.position = GetRandomPointCollider(groundCollider); // Set the obstacle position to the spawn point position
            obstaclePosition = obstacle.transform.position; */// Get the position of the obstacle
            Destroy(obstacle, 60); // Destroy after 500 seconds
        }
    }

    void SpawnEnemy()
    {

        if (tileIndex < 1) return; // Don't spawn enemies on the first tile
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
        Destroy(enObj, 20);
    }

    void SpawnPickUps()
    {
        int pickupsToSpawn = 6; // Number of pickups to spawn
        GameObject[] pickups = { healthPrefab, coalPrefab, pointsPrefab, timeorbPrefab }; // Array of pickup prefabs

        /*for (int i = 0; i < pickupsToSpawn; i++)
        {
            int selectedPickUp = Random.Range(0, pickups.Length); // Randomly select a pickup prefab from the array
            GameObject temp = Instantiate(pickups[selectedPickUp]);
            //Vector3 spawnPosition = GetRandomPointCollider(groundCollider); // Set the position of the pickup to a random point within the collider
            // Keep generating a new position if it's too close to the obstacle
            Vector3 spawnPos = GetSpacedPoint(groundCollider);
            while (Vector3.Distance(spawnPos, obstaclePosition) < 1.0f)
            {
                spawnPos = GetSpacedPoint(groundCollider);
                //spawnPosition = GetRandomPointCollider(groundCollider);// Check if the coin position is the same as the obstacle position
            }
            //temp.transform.position = spawnPosition;
            temp.transform.position = spawnPos;
            Destroy(temp, 60); // Destroy after 500 seconds
        }*/
        for (int i = 0; i < pickupsToSpawn; i++)
        {
            int selected = Random.Range(0, pickups.Length);
            GameObject pickup = Instantiate(pickups[selected]);

            Vector3 spawnPos = GetSpacedPoint(groundCollider);
            pickup.transform.position = spawnPos;

            Destroy(pickup, 60);
        }
        /*int pickUpsToSpawn = Random.Range(1, 4); // Randomly select the number of coins to spawn
        for (int i = 0; i < pickUpsToSpawn; i++)
        {
            Vector3 pickUpPosition = GetRandomCoinPointCollider(GetComponent<Collider>());  // Set the position of the coin to a random point within the collider

            // Keep generating a new position if it's too close to the obstacle
            while (Vector3.Distance(pickUpPosition, obstaclePositions) < 1.0f)
            {
                pickUpPosition = GetRandomCoinPointCollider(GetComponent<Collider>());// Check if the coin position is the same as the obstacle position
            }

            // Random Y rotation but no rotation in X and Z
            Quaternion pickUpRotation = Quaternion.Euler(90, 0, 0);
            // Randomly choose one of the pickup prefabs
            GameObject selectedPrefab = pickupPrefab[Random.Range(0, pickupPrefab.Length)];
            GameObject temp = Instantiate(selectedPrefab, pickUpPosition, pickUpRotation, transform); // Spawn coin
            PickUpCoin pickUpScript = temp.GetComponent<PickUpCoin>();

            if (pickUpScript != null)
            {
                int typeCount = System.Enum.GetValues(typeof(PickUpCoin.PickUpType)).Length;
               // pickUpScript.pickUpType = (PickUpCoin.PickUpType)Random.Range(0, typeCount);
                //pickUpScript.pickUpType = pickUpType;// Set the pickup type
            }
        }*/
    }
    Vector3 GetRandomPointCollider(Collider collider)
    {
        Vector3 randomPoint = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
        ); // Generate a random point within the bounds of the collider
        if (randomPoint != collider.ClosestPoint(randomPoint))
        {
            randomPoint = GetRandomPointCollider(collider); // Recursively call the method until a valid point is found
        }
        randomPoint.y = 1; // Set the Y coordinate to 1, matching it to the ground level
        return randomPoint;
    }

    Vector3 GetCenterPointCollider(Collider collider)
    {
        Vector3 center = collider.bounds.center;
        center.y = 1f;// Set the Y coordinate to 1, matching it to the ground level
        return center;
    }
    Vector3 GetSpacedPoint(Collider collider)
    {
        Vector3 point = GetRandomPointCollider(collider);// Generate a random point within the collider bounds
        int attempts = 0;

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
            float xDiff = Mathf.Abs(point.x - used.x);
            float zDiff = Mathf.Abs(point.z - used.z);

            if (xDiff < minDistance || zDiff < minDistance)
            {
                
                return false; // Too close in both X and Z
            }
        }
        Debug.Log("Point is far enough from all used positions.");

        return true;
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