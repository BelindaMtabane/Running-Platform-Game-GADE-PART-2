using UnityEngine;
using UnityEngine.UI; // Import the UI namespace to use UI elements
using UnityEngine.SceneManagement; // Import the SceneManagement namespace to manage scenes
using System.Collections;
using TMPro;
using System; // Import the Collections namespace for IEnumerator

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

    private void HandlePickupActivated(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    void OnCollisionEnter(Collision collision)
    {

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
                    playerMovement.AddCoal(increaseCoal); // Call the AddShield method in the PlayerMovement script
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
        RegisterPickupActivated();// Register the pickup activation event

        // Update the UI text
        playerMovement.UpdateUI();

        Destroy(gameObject); // Destroy the coin object
    }
    private void RegisterPickupActivated()
    {
        GameManager.Instance.LogPickupActivated(pickUpType.ToString()); // Log the pickup type to the console
    }
}
