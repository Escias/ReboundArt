using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObjective : MonoBehaviour
{
    [SerializeField] public Camera m_mainCamera;
    [SerializeField] public GameObject m_Objective;
    [SerializeField] public GameObject m_Ball;
    public Vector3 offset;
    public GameManager m_GameManager;
    public float speed = 5f;
    private bool hasReachObjective;
    private bool hasReachBall;
    private bool showBall;
    private bool showObjective;

    // Start is called before the first frame update
    void Start()
    {
        hasReachObjective = false;
        hasReachBall = false;
        showBall = true;
        showObjective = true;
        if (m_mainCamera == null)
        {
            m_mainCamera = Camera.main;
        }
        m_GameManager.SetGameStart(false);
        StartCoroutine(WaitStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_GameManager.GetGameStart() && !hasReachObjective && !showBall)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_Objective.transform.position + offset, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, m_Objective.transform.position + offset) < 0.01f)
            {
                StartCoroutine(WaitChange());
            }
        }
        if (!m_GameManager.GetGameStart() && !hasReachBall && hasReachObjective && !showObjective)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_Ball.transform.position + offset, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, m_Ball.transform.position + offset) < 0.01f)
            {
                hasReachBall = true;
                m_GameManager.SetGameStart(true);
            }
        }
    }

    IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(2f);
        hasReachObjective = false;
        showBall = false;
    }

    IEnumerator WaitChange()
    {
        yield return new WaitForSeconds(2f);
        hasReachObjective = true;
        showObjective = false;
    }
}
