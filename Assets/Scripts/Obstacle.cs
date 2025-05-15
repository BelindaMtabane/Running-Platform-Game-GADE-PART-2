using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //[SerializeField] GameObject thePlayer;//Associate with the player
    // public ObstacleTypes obstacleTypes;
    //[SerializeField] private Obstacle obstacleScript; // Reference to the Obstacle script
    //PlayerMovement playerMovement;// Call the Player movement class
    [SerializeField] GameObject deathMenu; // Reference to the Death Menu (assigned in the Inspector)
    //[SerializeField] GameObject woodenSpikes; // Reference to the Wooden Spike (assigned in the Inspector)
    //[SerializeField] GameObject portalGates; // Reference to the Evil Hands (assigned in the Inspector)
    //private Rigidbody playerRigid;//This will be attached to the Player to allow gravity to interact on the Player's physics
    //[SerializeField] EvilHands evilHands; // Reference to the EvilHands script
    //[SerializeField] WoodenSpike woodenSpike; // Reference to the WoodenSpike script
    //private PortalGateWay portalGateWay; // Reference to the PortalGateWay script*/

    public void EnableMenu()
    {
        deathMenu.SetActive(true);// This will make the death menu appear
    }

    void Start()
    {
    }
    /*public enum ObstacleTypes
    {
        EvilHands,
        Spike
    }*/
    void Update()
    {

        /*evilHands.EvilHandMovement(); // Call the EvilHandMovement method
        woodenSpike.WoodenSpikeMovement(); // Call the Update method of the WoodenSpike script*/
    }
    void OnCollisionEnter(Collision collision)
    {
        // Log the name of the object the player is colliding with
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        // Check if the player collides with the Spike or EvilHands

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Check if the the death menu is null to make it active or create a debug message
            if (deathMenu != null)
            {
                Debug.Log("Death Menu is assigned!");
                // Update the Death Menu score
                Time.timeScale = 0; // Freezes game
                                    //deathMenu.SetActive(true); // This makes the death menu appear

                EnableMenu(); // Call the EnableMenu method to show the death menu
            }
            else
            {
                Debug.LogError("Death Menu is not assigned!");// This will display an error message
            }
        }
    }
    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject); // Check if the object that entered the trigger is an obstacle, so we destroy the coin
            return; // Exit the method if the object is an obstacle
        }

        if (other.gameObject.name != "Player") return; // Check if the object that entered the trigger is the player
        //Destroy(gameObject);

        switch (obstacleTypes)
        {
            case ObstacleTypes.EvilHands:
                // Add health to the player
                // playerMovement.AddHealth(-10); // Call the AddHealth method in the PlayerMovement script
                Time.timeScale = 0; // Freezes game
                deathMenu.SetActive(true);
                break;
            case ObstacleTypes.Spike:
                // Add shield to the player
                Time.timeScale = 0; // Freezes game
                deathMenu.SetActive(true);
                //playerMovement.AddShield(-10); // Call the AddShield method in the PlayerMovement script
                break;
        }
        // Update the UI text
        //playerMovement.UpdateUI();

        //Destroy(gameObject); // Destroy the coin object
    }*/
}
