using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public float finalTime;
    float timer;
    int seconds, minutes;

    public TextMeshProUGUI timeText;

    void Start()
    {
        finalTime = 0;
        timer = 0;
        seconds = 0;
        minutes = 0;
    }

    void Update()
    {

        if (GameManager.gameEnded)
        {
            finalTime = timer;
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            return;
        }

        timer += Time.deltaTime;
        finalTime = timer;

        minutes = Mathf.FloorToInt(timer / 60f);
        seconds = Mathf.FloorToInt(timer - minutes * 60);
        

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
