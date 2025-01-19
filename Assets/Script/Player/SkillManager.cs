using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private bool skill1Active = false;
    private bool skill2Active = false;
    private bool skill3Active = false;
    public GameObject skill1;
    public GameObject skill2;
    public GameObject skill3;
    public PlayerSkill playerSkill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerSkill.GetCooldownRed() > 0)
        {
            skill1.SetActive(true);
        }
        else
        {
            skill1.SetActive(false);
        }
        if (playerSkill.GetCooldownGreen() > 0)
        {
            skill2.SetActive(true);
        }
        else
        {
            skill2.SetActive(false);
        }
    }

    public void CoolDownSkill(string skill)
    {
        switch(skill)
        {
            case "3":
                StartCoroutine(StartCoolDown(3, skill3Active, skill3));
                break;
        }
    }

    IEnumerator StartCoolDown(int time, bool skillBool, GameObject skill)
    {
        playerSkill.SetShockActive(false);
        skillBool = false;
        skill.SetActive(true);
        yield return new WaitForSeconds(time);
        skillBool = true;
        playerSkill.SetShockActive(true);
        skill.SetActive(false);
    }
}
