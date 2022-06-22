using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherScript : MonoBehaviour
{
    private void Start()
    {
        TestScript.instance.onCupBreak += OnCupBreak;
    }

    private void OnCupBreak()
    {
        Debug.Log("Injury");
    }
}
