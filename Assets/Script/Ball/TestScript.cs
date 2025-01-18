using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float stretchFactor = 1.5f;  // Maximum stretch factor based on impact
    public float squishFactor = 0.5f;  // Squish factor perpendicular to the stretch
    public float recoverySpeed = 2f;   // Speed of returning to original scale

    private Vector3 originalScale;
    private Rigidbody rb;

    void Start()
    {
        originalScale = transform.localScale; // Store the ball's initial scale
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Get the collision normal
        Vector3 collisionNormal = collision.contacts[0].normal;

        // Calculate the impact velocity along the normal
        float impactSpeed = Vector3.Dot(rb.velocity, collisionNormal);

        // Stretch along the normal direction, and squish in other directions
        Vector3 stretch = collisionNormal * Mathf.Abs(impactSpeed) * stretchFactor;
        Vector3 squish = Vector3.one - collisionNormal * squishFactor; // Squish perpendicular to the normal

        // Apply the deformation
        transform.localScale = new Vector3(
            originalScale.x * squish.x + stretch.x,
            originalScale.y * squish.y + stretch.y,
            originalScale.z * squish.z + stretch.z
        );
    }

    void OnCollisionExit(Collision collision)
    {
        // Start recovering the scale when leaving the surface
        StartCoroutine(RecoverScale());
    }

    System.Collections.IEnumerator RecoverScale()
    {
        while (Vector3.Distance(transform.localScale, originalScale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * recoverySpeed);
            yield return null;
        }

        transform.localScale = originalScale; // Snap back to exact original scale
    }
}
