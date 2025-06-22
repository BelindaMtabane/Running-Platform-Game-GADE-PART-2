using UnityEngine;

public class GunOperation : MonoBehaviour
{
    public GameObject bulletPrefab;
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script
    public Transform firePoint;
    public float bulletForce = 40f;
    public Transform cameraTransform;
    AudioManager audioManager; // Reference to the AudioManager script
    Enemy enemy; // Reference to the EnemyLocomotion script

    private void Start()
    {
        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();
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
        if (Input.GetMouseButtonDown(0)) // Left click to shoot
        {
            audioManager.PlaySoundEffects(audioManager.shooting); // Play the shoot sound effect
            Shoot();// Call the Shoot method when the left mouse button is pressed
        }
    }

    void Shoot()
    {
        // Check if the playerMovement script is assigned and has ammo left
        if (playerMovement.coal <= 0)
        {
            Debug.Log("Out of ammo!");
            return;
        }
        // Check if the bulletPrefab is assigned
        playerMovement.coal -= 10;
        // Log the ammo count after shooting
        Debug.Log("Ammo left: " + playerMovement.coal);
        // Instantiate a bullet at the fire point position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Shoot in camera direction
        Rigidbody rigid = bullet.GetComponent<Rigidbody>();
        Vector3 shoot = cameraTransform.forward;// Get the forward direction of the camera
        rigid.AddForce(shoot * bulletForce, ForceMode.Impulse);

        Destroy(bullet, 1f); // Clean up
    }
}
