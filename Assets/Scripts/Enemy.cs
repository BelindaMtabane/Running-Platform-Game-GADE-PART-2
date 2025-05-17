
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the Inspector
    public int enemyHealth = 50; // Set this value as needed

    private PlayerMovement playerMovement;
    [SerializeField] GameObject deathMenu;
    [SerializeField] TMP_Text ObstacleCollision;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    public void EnableMenu()
    {
        GameObject deathMenu = GameObject.FindGameObjectWithTag("DeathMenu");
        deathMenu.SetActive(true);// This will make the death menu appear
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Keep only the horizontal direction
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Log the name of the object the player is colliding with
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        // Check if the player collides with the Spike or EvilHands

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy collided with the Player!");

            // Example: Call a method on the player to handle the collision
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if ( enemyHealth > playerMovement.coal && playerMovement != null)
            {
                Time.timeScale = 0; // Freezes game
                playerMovement.KillPlayer();
            }else
            {
                // Destroy the enemy
                Destroy(gameObject); // Destroy the enemy object
                playerMovement.DeductCoal(enemyHealth); // Deduct coal the player used to kill enemy
            }
        
        }
    }
}


