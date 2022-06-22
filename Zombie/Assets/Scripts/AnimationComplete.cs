using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComplete : MonoBehaviour
{
    public delegate void OnComplete();
    public event OnComplete onComplete;

    public void OnCompleteCallback()
    {
        onComplete?.Invoke();
    }
}
