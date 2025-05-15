using UnityEngine;

/*public class PortalGateWay : Obstacle
{
    public GameObject portalPrefab; // Prefab for the portal
    public Transform spawnDestination; // Spawn point for the portal

    public new void OnCollisionEnter(Collision collision)
    {
        // Check if the object entering the trigger is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            if (spawnDestination != null)
            {
                // Instantiate the portal prefab at the current position and rotation
                GameObject portal = Instantiate(portalPrefab, transform.position, transform.rotation);
                //Check if PLayer collides with the portal
                collision.gameObject.transform.position = spawnDestination.position; // Move the player to the spawn destination
                
            }
            else
            {
                Debug.LogError("Spawn destination is not assigned!"); // Log an error if the spawn destination is not set
            }
        }
        else
        {
            // Log the name of the object the player is colliding with
            Debug.Log("Collision detected with: " + collision.gameObject.name);
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        // Check if the object exiting the trigger is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Destroy the portal when the player exits the trigger
            Destroy(portalPrefab);
        }
    }
}*/
