using UnityEngine;

public class WoodenSpike : MonoBehaviour
{
    /*public GameObject woodenSpike; // Instance of the wooden spike prefab
    public float spikeSpeed = 5f; // Speed of the wooden spike
    private int spikeDirection; // Direction of the wooden spike movement

    public void WoodenSpikeMovement()
    {
        // Check if the player is within the trigger collider
        if (woodenSpike != null)
        {
            // Move the wooden spike in the specified direction
            woodenSpike.transform.Translate(Vector3.right * spikeSpeed * Time.deltaTime * spikeDirection);

            // Change the direction of the wooden spike when it reaches the specified boundaries
            if (transform.position.x > 5f)
            {
                // Move the spike left
                spikeDirection = -1;
            }
            else if (transform.position.x < -5f)
            {
                // Move the spike right
                spikeDirection = 1;
            }
        }
    }*/

    private Vector3 startPosition;
    public float moveDistance = 2f;    // How far it moves up/down
    public float moveSpeed = 1f;       // Speed of the movement
    public float delayBeforeMoving = 60f;

    private float timer = 0f;// Tracks the time since the script started
    private bool canMove = false;// Indicates whether the wooden spike can move
    private float moveTimer = 0f;

    void Start()
    {
        startPosition = transform.position;//  Store the initial position of the wooden spike
    }

    void Update()
    {
        timer += Time.deltaTime;// Increment the timer

        if (!canMove && timer >= delayBeforeMoving)
        {
            canMove = true;// Enable movement after the delay
            moveTimer = 0f;// Reset the move timer
        }

        if (canMove)
        {
            moveTimer += Time.deltaTime;// Increment the move timer
            float offset = Mathf.PingPong(moveTimer * moveSpeed, moveDistance);// Calculate the offset using PingPong function
            transform.position = startPosition + Vector3.right * offset;// Apply the offset to the wooden spike's position
        }
    }
}
