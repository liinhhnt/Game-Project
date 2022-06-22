using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float lastTime;
    private float timeScale;
    private float timer;
    [SerializeField] private Transform[] transformArray;
    const float moveWait = 2.0f;
    const float scaleWait = 4.0f;
    private int oldSeconds;
    void Start()
    {
        lastTime = Time.time;
        timeScale = 1;
        ResetTime();
        Camera mainCam = Camera.main;
        mainCam.orthographic = true;
        mainCam.orthographicSize = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.time);
        //float deltaTime = Time.time - lastTime - 1.0f;
        //if (deltaTime >=-0.1f && deltaTime <= 0.1f)
        //{
        //    Debug.Log(lastTime);
        //    lastTime = Time.time;
        //}

        //Print if full second has passed
        timer += Time.deltaTime;
        int seconds = (int)timer % 60;
        
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ResetTime();
        }

        if (seconds != oldSeconds)
        {
            if (seconds % moveWait == 0)
            {
                MoveObjects();
            }
            oldSeconds = seconds;
        }
        Debug.Log(seconds);

        //Pause
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeScale = 1 - timeScale;
            Time.timeScale = timeScale;
            Debug.Log("Spacebar pressed");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            float randTime = Random.Range(0.25f, 9.75f);
            StartCoroutine(RotateObjects(randTime));
        }

    }

    private void ResetTime()
    {
        timer = 0;
        oldSeconds = 0;
        //tai sao lai dat o day? lap lai sau moi scale Wait giay,
        //nhung ma reset time lai chi thuc hien hanh dong dung 1 lan
        //khi an Enter 
        InvokeRepeating("ScaleObjects", 0.001f, scaleWait);
    }
    private void MoveObjects()
    {
        for (int i=0; i<transformArray.Length; i++)
        {
            Vector3 pos = transformArray[i].position;
            if (pos.x * pos.y > 0) pos.y *= -1;
            else pos.x *= -1;
            transformArray[i].position = pos;
        }
    }
    private void ScaleObjects()
    {
        for (int i = 0; i < transformArray.Length; i++)
        {
            if (transformArray[i].localScale.x >= 1.5f)
            {
                transformArray[i].localScale /= 1.2f;
            }
            else
            {
                transformArray[i].localScale *= 1.2f;
            }
        }
    }
    IEnumerator RotateObjects (float randomDelay)
    {
        for (int j=0; j<4; j++)
        {
            yield return new WaitForSeconds(randomDelay);
            for (int i = 0; i < transformArray.Length; i++)
            {
                transformArray[i].Rotate(0f, 0f, 90f, Space.Self);
            }
        }
    }
}
