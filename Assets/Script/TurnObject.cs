using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnObject : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public bool rotateClockwise = true;

    void Update()
    {
        float direction = rotateClockwise ? 1f : -1f;
        transform.Rotate(0f, direction * rotationSpeed * Time.deltaTime, 0f);
    }
}
