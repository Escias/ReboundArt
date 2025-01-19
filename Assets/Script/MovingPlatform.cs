using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // The first point (A)
    public Transform pointB; // The second point (B)
    public float speed = 5f; // The movement speed

    private Vector3 targetPosition; // The current target position

    void Start()
    {
        // Initialize the target position as point B
        targetPosition = pointB.position;
    }

    void Update()
    {
        // Move the platform towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the platform reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            // Switch the target position to the other point
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
        }
    }
}