using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameStart;
    private bool ballStart;
    private bool firstAction;

    // Start is called before the first frame update
    void Start()
    {
        gameStart = false;
        ballStart = false;
        firstAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameStart(bool state)
    {
        gameStart = state;
    }

    public bool GetGameStart()
    {
        return gameStart;
    }

    public void SetBallStart(bool state)
    {
        ballStart = state;
    }

    public bool GetBallStart()
    {
        return ballStart;
    }
}
