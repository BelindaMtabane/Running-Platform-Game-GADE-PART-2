using UnityEngine;
using UnityEngine.UI; // Import the UI namespace to use UI elements
using UnityEngine.SceneManagement; // Import the SceneManagement namespace to manage scenes
using System.Collections;
using TMPro; // Import the Collections namespace for IEnumerator

public class PickUpCoin : MonoBehaviour
{
    public PickUpType pickUpType; // Type of the pickup
    [SerializeField] PlayerMovement playerMovement; // Reference to the PlayerMovement script


    [SerializeField] int increasePoints = 10; // Points to increase when collected
    [SerializeField] int increaseHealth = 10; // Health to increase when collected
    [SerializeField] int increaseCoal = 10; // Coal to increase when collected
    [SerializeField] int increaseTimeorb = 5; // Coal to increase when collected

    ///[SerializeField] int slowDownTimeForSeconds = 5; // Time to slow down the player when the coin is collected
    public enum PickUpType
    {
        Health,
        Coal,
        Points,
        TimeOrb
    }
    private GameObject[] pickUpPrefabs;//Prefab for the pick up

    void Start()
    {

        // Check if the object that entered the trigger is the player
        GameObject player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject with tag 'Player' not found! Assign the 'Player' tag in the Inspector.");
            return;
        }

        playerMovement = player.GetComponent<PlayerMovement>();

        pickUpPrefabs = new GameObject[4]; // Initialize the array with 3 elements
    }
    void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject); // Check if the object that entered the trigger is an obstacle, so we destroy the coin
            return; // Exit the method if the object is an obstacle
        }*/

        if (collision.gameObject.CompareTag("Player"))
        {
            switch (pickUpType)
            {
                case PickUpType.Health:
                    // Add health to the player
                    playerMovement.AddHealth(increaseHealth); // Call the AddHealth method in the PlayerMovement script
                    break;
                case PickUpType.Coal:
                    // Add shield to the player
                    playerMovement.AddShield(increaseCoal); // Call the AddShield method in the PlayerMovement script
                    break;
                case PickUpType.TimeOrb:
                    // Add time to the player
                    playerMovement.AddTimeOrb(increaseTimeorb); // Call the AddTimeOrb method in the PlayerMovement script
                    break;
                case PickUpType.Points:
                    // Add points to the player
                    playerMovement.AddPoints(increasePoints); // Call the AddPoints method in the PlayerMovement script
                    break;

            }
        }
        // Update the UI text
        playerMovement.UpdateUI();

        Destroy(gameObject); // Destroy the coin object
    }

    /*public PickUpType pickUpType; // Type of the pickup
    private Renderer coinRenderer; // Renderer component of the coin
    [SerializeField] float turnSpeed = 90f; // Speed at which the coin rotates

    void OnTriggerEnter(Collider other)
    {  
        if (other.gameObject.GetComponent<Obstacle>() != null){
            Destroy(gameObject); // Check if the object that entered the trigger is an obstacle, so we destroy the coin
            return; // Exit the method if the object is an obstacle
        };

        if (other.gameObject.name != "Player") return; // Check if the object that entered the trigger is the player

        switch (pickUpType)
        {
            case PickUpType.Health:
                // Add health to the player
                playerMovement.AddHealth(increaseHealth); // Call the AddHealth method in the PlayerMovement script
                break;
            case PickUpType.Shield:
                // Add shield to the player
                playerMovement.AddShield(increaseShield); // Call the AddShield method in the PlayerMovement script
                break;
            case PickUpType.TimeOrb:
                // Add time to the player
                playerMovement.AddTimeOrb(slowDownTimeForSeconds); // Call the AddTimeOrb method in the PlayerMovement script
                break;
            case PickUpType.Points:
                // Add points to the player
                playerMovement.AddPoints(increasePoints); // Call the AddPoints method in the PlayerMovement script
                break;
        }

        // Update the UI text
        playerMovement.UpdateUI();

        Destroy(gameObject); // Destroy the coin object
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GameObject.FindAnyObjectByType<PlayerMovement>(); // Find the PlayerMovement script in the scene

        coinRenderer = GetComponent<Renderer>();
        if (coinRenderer != null)
        {
            switch (pickUpType)
            {
                case PickUpType.Health:
                    coinRenderer.material.color = Color.green;
                    break;
                case PickUpType.Shield:
                    coinRenderer.material.color = Color.blue;
                    break;
                case PickUpType.TimeOrb:
                    coinRenderer.material.color = Color.black;
                    break;
                case PickUpType.Points:
                    coinRenderer.material.color = Color.yellow;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime); // Rotate the coin around its Y-axis
    }*/
}
