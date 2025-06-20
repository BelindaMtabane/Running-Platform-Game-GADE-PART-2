using UnityEngine;

public class EvilHands : MonoBehaviour
{
    private Vector3 originalScale;
    public float scaleAmount = 2f; // How much to grow/shrink
    public float scaleSpeed = 1f;    // Speed of the pulsing
    public float delayBeforeScaling = 30f; // Delay in seconds before scaling starts

    private float timer = 0f;// Tracks the time since the script started
    private bool canScale = false;
    private float scaleTimer = 0f; // Tracks time *after* scaling starts


    void Start()
    {
        originalScale = transform.localScale;// Store the original scale of the EvilHands object
        gameObject.SetActive(true); // Activate hand object
    }

    void Update()
    {
        timer += Time.deltaTime;// Increment the timer

        if (!canScale && timer >= delayBeforeScaling)
        {
            canScale = true;// Enable scaling after the delay
            scaleTimer = 0f; // reset scale timer
        }

        if (canScale)
        {
            Debug.Log("EvilHands scaling at: " + Time.time + " seconds");// Log the time when scaling starts
            scaleTimer += Time.deltaTime;// Increment the scale timer

            float scale = Mathf.PingPong(scaleTimer * scaleSpeed, scaleAmount) + 1f;// Calculate the scale based on the ping-pong function
            transform.localScale = originalScale * scale;// Apply the scale to the EvilHands object
        }
    }
}