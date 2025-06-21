
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the Inspector
    public int enemyHealth = 50; // Set this value as needed
    private PlayerMovement playerMovement;
    [SerializeField] GameObject deathMenu;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] TMP_Text ObstacleCollision;
    public Animator animator;

    private bool isPlayerInTrigger = false;
    [SerializeField] float followSpeed = 3f; // Adjust as needed

    void Start()
    {
        playerMovement = PlayerMovement.Instance; // Get the PlayerMovement instance
        player = playerMovement.transform; // Assign the player's transform
        animator = GetComponent<Animator>();
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

        if (isPlayerInTrigger && player != null)
        {
            // Move towards the player
            direction.y = 0; // Keep movement horizontal
            transform.position += direction * followSpeed * Time.deltaTime;

            // Look at the player
            if (direction != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Log the name of the object the player is colliding with
        Debug.Log("Collision detected with: " + other.gameObject.name);
        
        // Check if the player collides with the Spike or EvilHands

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy collided with the Player!");

            playerMovement.StopMovement();
            isPlayerInTrigger = true; // Set the flag to true when the player enters the trigger

            // Example: Call a method on the player to handle the collision
            if (enemyHealth > playerMovement.coal && playerMovement != null)
            {
                animator.SetTrigger("Attack"); // Trigger the attack animation
                Invoke("ResetBattleCryttack", 2.0f);
            }
            else
            {
                // Destroy the enemy
                animator.SetTrigger("Die"); // Trigger the death animation
                Invoke("ContinueGame", 2.0f); // Continue the game after 2 seconds
            }

        }
    }

    void ResetBattleCryttack()
    {
        playerMovement.KillPlayer();
        animator.SetTrigger("BattleCry");
        Time.timeScale = 0; // Freezes game
                            //deathMenu.SetActive(true); // This makes the death menu appear

        EnableMenu(); // Call the EnableMenu method to show the death menu
                      // Update the UI text
        ObstacleCollision.text = "You have been killed by the Monster";
    }

    void ContinueGame()
    {
        playerMovement.DeductCoal(enemyHealth); // Deduct coal the player used to kill enemy
        playerMovement.StartMovement(); // Resume player movement
    }
}


