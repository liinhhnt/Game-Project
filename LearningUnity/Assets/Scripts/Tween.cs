using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween 
{
    //private Transform targetObject;
    //private float time;

    public Transform Target { get; private set; }
    public Vector3 StartPos { get; private set; }
    public Vector3 EndPos { get; private set; }
    public float StartTime { get; private set; }
    public float Duration { get; private set; }

    public Tween(Transform targetObject, Vector3 startPos, Vector3 endPos, float time, float duration)
    {
        this.Target = targetObject;
        this.StartPos = startPos;
        this.EndPos = endPos;
        this.StartTime = time;
        this.Duration = duration;
    }
}
