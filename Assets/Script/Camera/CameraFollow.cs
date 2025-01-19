using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject m_Ball;
    public float smooth;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    public GameManager m_gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 targetPosition = m_Ball.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smooth);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Ball != null && m_gameManager.GetGameStart())
        {
            Vector3 targetPosition = m_Ball.transform.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smooth);
        }
    }
}
