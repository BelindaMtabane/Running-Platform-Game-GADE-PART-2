
using System;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the Inspector
    public int enemyHealth = 50; // Set this value as needed
    private PlayerMovement playerMovement;
   // [SerializeField] GameObject deathMenu;
    Obstacle obstacleScript; // Reference to the Obstacle script
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] TMP_Text ObstacleCollision;
    public Animator animator;
    GameManager gameManager; // Reference to the GameManager script
    AudioManager audioManager; // Reference to the AudioManager script
    private bool isPlayerInTrigger = false;
    [SerializeField] float followSpeed = 3f; // Adjust as needed
    public static int beatenScore = 0;
    public static event Action<int> OnEnemyBeaten; // Sends enemyScore to subscribers


    void Start()
    {
        playerMovement = PlayerMovement.Instance; // Get the PlayerMovement instance
        player = playerMovement.transform; // Assign the player's transform
        animator = GetComponent<Animator>();
        obstacleScript = GameObject.FindAnyObjectByType<Obstacle>(); // Find the Obstacle script in the scene
        if (audioManager != null)
        {
            audioManager.PlaySoundEffects(audioManager.enemy);
        }
        gameManager = GameObject.FindAnyObjectByType<GameManager>(); // Find the GameManager script in the scene

    }

    private void Awake()
    {
        audioManager = GameObject.FindFirstObjectByType<AudioManager>(); // Find the AudioManager in the scene
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene!"); // Log an error if AudioManager is not found
        }
    }

    void Update()
    {
        //audioManager.PlaySoundEffects(audioManager.enemy); // Play the enemy sound effect
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
                Debug.Log("Enemy killed the player by attack!");
                Invoke("ResetBattleCryttack", 2.0f);
            }
            else
            {
                // Destroy the enemy
                animator.SetTrigger("Die"); // Trigger the death animation
                Debug.Log("Enemy defeated!");
                beatenScore++;
                Debug.Log("Boss beaten score incremented by " + beatenScore);
                // Fire event
                OnEnemyBeaten?.Invoke(beatenScore);
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
        Debug.Log("ResetBattleCryttack called");
        //obstacleScript.EnableMenu(); // Call the EnableMenu method to show the death menu
        // Update the UI text
       // ObstacleCollision.text = "You have been killed by the Monster";
    }

    void ContinueGame()
    {
        playerMovement.DeductCoal(enemyHealth); // Deduct coal the player used to kill enemy
        playerMovement.StartMovement(); // Resume player movement
        Destroy(gameObject); // Destroy the enemy game object
    }
}


