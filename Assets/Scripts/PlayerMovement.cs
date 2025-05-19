using UnityEngine;
using UnityEngine.UI; // Import the UI namespace to use UI elements
using UnityEngine.SceneManagement; // Import the SceneManagement namespace to manage scenes
using System.Collections;
using TMPro; // Import the Collections namespace for IEnumerator
/*
// Add these fields at the top of your PlayerMovement class
[SerializeField] private TMP_Text clockText; // Assign this in the Inspector

// In your Update() method, add this at the end (after all other logic):
if (isPlayerMoving && isAlive)
{
    elapsedTime += Time.deltaTime;
    UpdateClockUI();
}*/
public class PlayerMovement : MonoBehaviour
{
    public bool isAlive = true; // This is a flag to check if the player is alive
    [SerializeField] public float speed = 9f; // This is the speed of the player
    public new Rigidbody rigidbody; // Reference to the Rigidbody component

    [SerializeField] int maxTimeOrb = 10; // This is the maximum time orb of the player
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text deathScore;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text coalText;
    [SerializeField] private TMP_Text clockText; // Assign this in the Inspector
    float elapsedTime = 0f; // This is the elapsed time since the game started

    public int points = 0; // This is the score of the player
    public int health = 50000; // This is the health of the player
    [SerializeField] int maxHealth = 100; // This is the maximum health of the player
    public int coal = 0; // This is the coal of the player

    [SerializeField] int maxCoal = 100; // This is the maximum coal of the player
    private int timeOrb = 0; // This is the time orb of the player
    [SerializeField] private Obstacle obstacle;// Reference to the Obstacle script

    float horizontalInput;
    [SerializeField] float horizontalSpeedMultiplier = 1.5f; // Multiplier for horizontal speed

    // Add these fields to set X axis movement limits
    [SerializeField] private float minX = -8.90f;
    [SerializeField] private float maxX = 8.75f;

    [SerializeField] private TMP_Text countdownText; // Reference to a TextMeshProUGUI element to display the countdown

    public float jumpVelocity = 0.21f;//This is a variable that determines the force of the jump
    public float downVelocity = -0.21f;//This is a variable that determines the force of the downward movement
    public float jumpMax = 2.1f; // Height at which downward force is applied
    public float delayForce = 0.21f; // Delay before downward force is applied
    private float airTime; // Time when jump occurred

    [SerializeField] private float countdownTimer = 3f; // Countdown time in seconds

    private bool isPlayerMoving = false; // Flag to track if the player is moving

   // GameObject deathMenu;

    void Start()
    {
        //deathMenu = GameObject.FindGameObjectWithTag("DeathMenu");
        //deathMenu.SetActive(false); // Hide the death menu at the start
        rigidbody = GetComponent<Rigidbody>();//This will control the PLayer's position in the game
        obstacle = GetComponent<Obstacle>();// This will control the Obstacle's position in the game
       
    }
    public void StartCountdown()
    {
        countdownText.text = "Starting in: " + countdownTimer.ToString(); // Show initial countdown
        // Call the method to start moving Player
        StartMovement();// Start moving
    }
    public void StartMovement()
    {
        isPlayerMoving = true; // Set to true for player to move
        countdownText.gameObject.SetActive(false); // Hide the countdown text
        //FixedUpdate();
    }

    // Modify FixedUpdate to clamp the player's X position after movement
    // Add this field to track the health drain timer
    private float healthDrainTimer = 0f;

    void FixedUpdate()
    {
        if (!isAlive) return; // If the player is not alive, do not move

        Vector3 moveDelay = new Vector3(0, 0, 0); // Initialize the movement vector
        // Handle countdown timer
        if (countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime; // Decrement countdown time
            countdownText.text = "Starting in: " + Mathf.Ceil(countdownTimer).ToString(); // Update the countdown text
        }
        else if (!isPlayerMoving)
        {
            StartMovement(); // Start moving when the countdown ends
            isPlayerMoving = true; // Set to true for player to move
        }
        if (isPlayerMoving)
        {
            // Health drain logic
            healthDrainTimer += Time.fixedDeltaTime;
            // Check if the health drain timer has reached 1 second
            if (healthDrainTimer >= 1f)
            {
                health -= 1;// This is the health drain per second
                healthDrainTimer = 0f;// Reset the timer
                if (health < 0) health = 0;// Ensure health does not go below 0
                UpdateUI();// Update the UI with the new health value
                if (health <= 0)
                {
                    Time.timeScale = 0; // Freezes game
                    // Call the KillPlayer method to handle player death
                    obstacle.EnableMenu();
                    return;
                }
            }

            Vector3 forwardMovement = transform.forward * speed * Time.fixedDeltaTime;// This is the forward movement of the player
            Vector3 horizontalMovement = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalSpeedMultiplier;// This is the horizontal movement of the player
            Vector3 newPosition = rigidbody.position + forwardMovement + horizontalMovement;// This is the new position of the player

            // Clamp the X position
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

            rigidbody.MovePosition(newPosition);// This will move the player to the new position
        }
        Update(); // Call the Update method to handle input and other updates
    }
    void Update()
    {
        if (!isAlive) return; // If the player is not alive, do not process input

        // Handle player movement
        if (isPlayerMoving)
        {
            horizontalInput = Input.GetAxis("Horizontal");

            elapsedTime += Time.deltaTime; // Increment elapsed time
            int minutes = Mathf.FloorToInt(elapsedTime / 60); // Calculate minutes
            int seconds = Mathf.FloorToInt(elapsedTime % 60); // Calculate seconds
            clockText.text = string.Format("{0:00} : {1:00}", minutes, seconds); // Update the clock UI

            // Check if the player falls below a certain height
            if (transform.position.y < -5) // Check if the player falls below a certain height
            {
                KillPlayer(); // Call the KillPlayer method
            }

            // Quite the game if the Escape key is pressed
            if (Input.GetKeyDown(KeyCode.Escape)) // Check if the Escape key is presse-d
            {
                Application.Quit(); // Quit the application
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                // Add force to make the player jump
                rigidbody.AddForce(new Vector3(0, jumpVelocity, 0), ForceMode.Impulse);
                airTime = Time.time; // Record the time when the jump occurred
            }
            if (transform.position.y >= jumpMax)
            {
                if (Time.time - airTime >= delayForce)
                {
                    // Apply downward force after a delay
                    rigidbody.AddForce(new Vector3(0, downVelocity, 0), ForceMode.Impulse);
                }

            }
        }
    }
    public void KillPlayer()
    {
       // deathMenu.SetActive(true);
        // This is method is currently not being used, it will be used when the player collides with a certain object
        isAlive = false; // Set the player to not alive
        // Restart the Game
       //  SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd; // Add points to the player's score
    }

    public void AddHealth(int healthToAdd)
    {
        health += healthToAdd; // Add health to the player's health
        if (health > maxHealth) // Ensure health does not exceed maxHealth
        {
            health = maxHealth;
        }
    }

    public void AddCoal(int coalToAdd)
    {

        coal += coalToAdd; // Add coal to the player's coal
        Debug.Log("Coal calld: " + coal); // Log the current coal value
        if (coal > maxCoal) // Ensure coal does not exceed maxCoal
        {
            Debug.Log("Max Coal: " + maxCoal); // Log the maximum coal value
            coal = maxCoal;
        }
    }

    public void DeductCoal(int coalToDeduct)
    {
        coal -= coalToDeduct;
        Debug.Log("Coal calld: " + coal); // Log the current coal value
        if (coal < 0) // Ensure coal does not exceed maxCoal
        {
            coal = 0;
        }
    }

    public void AddTimeOrb(int timeOrbToAdd)
    {
        timeOrb += timeOrbToAdd; // Add time orb to the player's time orb
        Debug.Log("Time Orb: " + timeOrb); // Log the current time orb value
        if (timeOrb > maxTimeOrb) // Ensure timeOrb does not exceed maxTimeOrb
        {
            Debug.Log("Max Time Orb: " + maxTimeOrb); // Log the maximum time orb value
            timeOrb = maxTimeOrb;
        }
        StartCoroutine(SlowDownPlayer(timeOrbToAdd)); // Trigger the slowdown effect
    }

    public IEnumerator SlowDownPlayer(int duration)
    {
        float originalSpeed = speed; // Store the original speed
        speed = 2f; // Reduce the player's speed
        yield return new WaitForSeconds(duration); // Wait for the specified duration
        speed = originalSpeed; // Revert to the original speed
    }


    public void UpdateUI()
    {
        Debug.Log("UpdateUI called"); // Log when the UI is updated
        deathScore.text = "Score: " + points; // Update the death score text
        scoreText.text = "Score: " + points; // Update the score text
        healthText.text = "Health: " + health; // Update the health text
        coalText.text = "Coal: " + coal; // Update the coal text
    }
}
