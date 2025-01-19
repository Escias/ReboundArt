using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideZone : MonoBehaviour
{
    [SerializeField] public GameObject ball;
    Vector3 initPosition;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, 0, 0);
            other.gameObject.transform.position = initPosition;
        }
    }
}
