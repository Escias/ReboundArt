using System;
using UnityEngine;

public class LineCollider : MonoBehaviour
{
    public float bounceMultiplier = 0f;
    public bool isSkillRed = false;
    public PhysicMaterial spherePhysicMaterial;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                if (isSkillRed)
                {
                    Vector3 collisionNormal = collision.contacts[0].normal;
                    Vector3 reboundForce = collisionNormal * -bounceMultiplier;

                    ballRigidbody.AddForce(reboundForce, ForceMode.Impulse);
                }
                else
                {
                    SphereCollider sphereCollider = collision.gameObject.GetComponent<SphereCollider>();
                    sphereCollider.material = new PhysicMaterial();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SphereCollider sphereCollider = other.gameObject.GetComponent<SphereCollider>();
        sphereCollider.material = spherePhysicMaterial;
    }
}