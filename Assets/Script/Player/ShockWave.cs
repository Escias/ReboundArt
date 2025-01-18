using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    private GameObject ball;
    BallMove m_ballMove;
    public float power;
    float explosionPower = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.tag == "Ball")
            {
                ball = other.gameObject;
                float distance = 10 / Vector3.Distance(ball.transform.position, transform.position);
                explosionPower = power * distance;
                m_ballMove = other.gameObject.GetComponent<BallMove>();
                m_ballMove.MoveBall(transform.gameObject, explosionPower);
            }
        }
    }
}
