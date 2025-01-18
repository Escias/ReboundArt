using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRaycast : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject drawZone;
    private Vector3 hitposition;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void FixedUpdate()
    {
        LayerMask layerMask = LayerMask.GetMask("DrawZone");
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject == drawZone)
            {
                hitposition = hit.point;
            }
        }
    }

    public Vector3 GetHitPosition()
    {
        return hitposition;
    }
}
