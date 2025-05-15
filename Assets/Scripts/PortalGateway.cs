using UnityEngine;

public class PortalGateWay : MonoBehaviour
{
    public GameObject portalPrefab; // Prefab for the portal
    //blic Transform spawnDestination; // Spawn point for the portal

    public void OnCollisionEnter(Collision collision)
    {
        // Check if the object entering the trigger is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            if (portalPrefab != null)
            {

                // Instantiate the portal prefab at the current position and rotation
                //GameObject portal = Instantiate(portalPrefab, transform.position, transform.rotation);
                //Check if PLayer collides with the portal
                //collision.gameObject.transform.position = spawnDestination.position; // Move the player to the spawn destination
                // Move player 6 units backward from their current facing direction
                Vector3 backDirection = collision.gameObject.transform.forward;
                collision.gameObject.transform.position += backDirection * 6f;
            }
            else if (portalPrefab == null)
            {
                Debug.LogError("Portal prefab is not assigned!"); // Log an error if the portal prefab is not set
            }

            else
            {
                // Log the name of the object the player is colliding with
                Debug.Log("Collision detected with: " + collision.gameObject.name);
            }

        }
    }
}
