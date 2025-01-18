using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    Rigidbody rb;
    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartMove()
    {
        if (manager.GetBallStart() && manager.GetGameStart())
        {
            rb.isKinematic = false;
        }
    }

    public void MoveBall(GameObject explo, float power)
    {
        Vector3 vector = rb.position - explo.transform.position;
        rb.velocity = new Vector3(vector.x * power, vector.y * power, 0) * Time.deltaTime;
    }
}
