using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public static TestScript instance;

    public delegate void OnCupBreak();
    public event OnCupBreak onCupBreak;

    public delegate void OnVideoComplete();
    public delegate void OnVideoFailed();

    float currentTime;
    float maxTime = 4;

    public IEnumerator ShowVideoAds(OnVideoComplete callback, OnVideoFailed callbackFail)
    {
        yield return new WaitForSeconds(2);
        Debug.Log("VideComplete");
        bool fail = true;
        if (fail)
            callbackFail?.Invoke();
        else
            callback?.Invoke();
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    OnCupBreakAction(CupBreakCallback);
        //}
    }

    private void CupBreakCallback()
    {
        Debug.Log("Cup break callback");
    }

    private void OnCupBreakAction(OnCupBreak callback)
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            currentTime = 0;
            callback?.Invoke();
        }
    }
}
