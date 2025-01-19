using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float elapsedTime = 0f;
    public bool timerIsRunning = false;
    public GameManager manager;
    public GameObject instruction;

    void Start()
    {
        instruction.SetActive(true);
        elapsedTime = 0f;
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning && manager.GetBallStart())
        {
            instruction.SetActive(false);
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay(elapsedTime);
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt((timeToDisplay % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        int hundredths = Mathf.FloorToInt((timeToDisplay * 100) % 100);
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
    }

    public void PauseTimer()
    {
        timerIsRunning = false;
    }

    public void ResumeTimer()
    {
        timerIsRunning = true;
    }
}
