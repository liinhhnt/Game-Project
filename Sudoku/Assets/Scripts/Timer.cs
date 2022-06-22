using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour
{
    public float time;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public static Timer Instance;
    void Start()
    {
        Instance = this;
        timeText = GetComponent<TextMeshProUGUI>();
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            time += Time.deltaTime;
            DisplayTime(time);
        }
        else
        {
            //Debug.Log("Pause");
            //timerIsRunning = false; 
        }
    }

    void DisplayTime (float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        //Debug.Log(minutes + " " + seconds) ;
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
