using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameStart;

    // Start is called before the first frame update
    void Start()
    {
        gameStart = false;
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
}
