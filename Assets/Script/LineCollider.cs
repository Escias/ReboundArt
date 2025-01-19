using System;
using System.Collections;
using UnityEngine;

public class LineCollider : MonoBehaviour
{
    public float bounceMultiplier = 8f;
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
                    ballRigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                    ballRigidbody.constraints = RigidbodyConstraints.None;
                    ballRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                }
            }
        }
    }
}