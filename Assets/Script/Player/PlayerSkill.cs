using System.Collections;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public MouseRaycast m_MouseRaycast;
    public GameObject m_ShockWave;
    public GameManager manager;
    public BallMove ballMove;
    
    public float cooldownDuration = 10f;
    private bool skillRedActive = false;
    private bool lineDrawnDuringSkillRed = false;
    private float skillRedCooldownTimer = 0f;
    
    private bool skillGreenActive = false;
    private bool lineDrawnDuringSkillGreen = false;
    private float skillGreenCooldownTimer = 0f;

    void Update()
    {
        if (manager.GetGameStart())
        {
            HandleCooldown();

            if (Input.GetKeyDown(KeyCode.A) && skillRedCooldownTimer <= 0)
            {
                StartCoroutine(ActivateSkillRed());
            }
            
            if (Input.GetKeyDown(KeyCode.Z) && skillGreenCooldownTimer <= 0)
            {
                StartCoroutine(ActivateSkillGreen());
            }

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

    public bool IsSkillRedActive()
    {
        return skillRedActive;
    }
    
    public bool IsSkillGreenActive()
    {
        return skillGreenActive;
    }

    public bool CanDrawLine()
    {
        return !lineDrawnDuringSkillRed; 
    }

    public void MarkLineDrawn()
    {
        lineDrawnDuringSkillRed = true;
        skillRedActive = false;
    }

    private void HandleCooldown()
    {
        if (skillRedCooldownTimer > 0)
        {
            skillRedCooldownTimer -= Time.deltaTime;
        }
    }

    private IEnumerator ActivateSkillRed()
    {
        skillRedActive = true;
        lineDrawnDuringSkillRed = false;
        skillRedCooldownTimer = cooldownDuration;

        while (!lineDrawnDuringSkillRed)
        {
            yield return null;
        }

        skillRedActive = false;
    }
    
    private IEnumerator ActivateSkillGreen()
    {
        skillGreenActive = true;
        lineDrawnDuringSkillGreen = false;
        skillGreenCooldownTimer = cooldownDuration;

        while (!lineDrawnDuringSkillGreen)
        {
            yield return null;
        }

        skillGreenActive = false;
    }

    IEnumerator UseShockWave()
    {
        GameObject shockWave = Instantiate(m_ShockWave, m_MouseRaycast.GetHitPosition(), Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Destroy(shockWave);
    }
}
