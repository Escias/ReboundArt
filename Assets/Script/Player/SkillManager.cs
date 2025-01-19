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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CoolDownSkill(string skill)
    {
        switch(skill)
        {
            case "1":
                StartCoroutine(StartCoolDown(3, skill1Active, skill1));
                break;
            case "2":
                StartCoroutine(StartCoolDown(3, skill2Active, skill2));
                break;
            case "3":
                StartCoroutine(StartCoolDown(3, skill3Active, skill3));
                break;
        }
    }

    IEnumerator StartCoolDown(int time, bool skillBool, GameObject skill)
    {
        skillBool = false;
        skill.SetActive(true);
        yield return new WaitForSeconds(time);
        skillBool = true;
        skill.SetActive(false);
    }
}
