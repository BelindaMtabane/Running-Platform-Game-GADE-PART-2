using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target to follow
    public Vector3 offset; // The offset from the target position

    void Start()
    {
        offset = transform.position - target.position; // Calculate the initial offset   
    }

    void Update()
    {
        Vector3 targetPosition = target.position + offset; // Calculate the target position
        targetPosition.x = 0;
        transform.position = targetPosition; // Update the camera position
    }
}