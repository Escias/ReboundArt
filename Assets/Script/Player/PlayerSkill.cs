using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public MouseRaycast m_MouseRaycast;
    public GameObject m_ShockWave;
    public GameManager manager;
    public BallMove ballMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.GetGameStart())
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (!manager.GetBallStart())
                {
                    manager.SetBallStart(true);
                    ballMove.StartMove();
                }
                StartCoroutine(UseShockWave());
            }
        }
    }

    IEnumerator UseShockWave()
    {
        GameObject shockWave = Instantiate(m_ShockWave, m_MouseRaycast.GetHitPosition(), Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Destroy(shockWave);
    }
}
