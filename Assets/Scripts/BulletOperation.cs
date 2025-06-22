using UnityEngine;

public class BulletOperation : MonoBehaviour
{
    public int damage = 10;

    void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            //enemy.TakeDamage(damage); // Apply bullet damage
            // If the enemy's health is zero or less, you can destroy it
            Debug.Log("Enemy defeated!");
        }

        // Destroy the bullet
        Destroy(gameObject);

    }
}
