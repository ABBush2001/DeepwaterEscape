using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerAlterDisplay : MonoBehaviour
{
    public float timeLimit = 10f;
    private float curTimeRemaining;
    public float endTime = 0f;
    public bool timerRunning = false;
    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerRunning = false;
        curTimeRemaining = timeLimit;
    }
    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            if (curTimeRemaining > endTime)
            {
                curTimeRemaining -= Time.deltaTime;
                DisplayTime(curTimeRemaining);
            }
            else
            {
                timerRunning = false;
                curTimeRemaining = 0;
                timerText.text = string.Format("00:00");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); //2
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); //10
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
