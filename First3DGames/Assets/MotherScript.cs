using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherScript : MonoBehaviour
{
    private void Start()
    {
        TestScript.instance.onCupBreak += CupBreak; 
    }

    private void CupBreak()
    {
        Debug.Log("Chui");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnShowVideo();
        }
    }

    void OnShowVideo()
    {
        //StartCoroutine(TestScript.instance.ShowVideoAds(OnVideoComplete, OnVideoFail));
        StartCoroutine(TestScript.instance.ShowVideoAds(delegate
        {
            Debug.Log("Add reward to player");
        }, delegate
        {
            Debug.Log("Failed video");
        }));
    }

    void OnVideoComplete()
    {
        Debug.Log("Add reward to player");
    }

    void OnVideoFail()
    {
        Debug.Log("Failed video");
    }
}
