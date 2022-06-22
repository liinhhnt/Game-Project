using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    //private Tween activeTween;
    private List<Tween> activeTweens = new List<Tween>();
    public bool TweenExists(Transform target)
    {
        for (int i = 0; i < activeTweens.Count; i++) 
            if (ReferenceEquals(activeTweens[i].Target, target)) 
                return true;
            //if (activeTweens[i].Target == target) return true;
        return false;
        
    }
    public bool AddTween (Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
       // Debug.Log(TweenExists(targetObject) + targetObject.gameObject.name);
        if (!TweenExists(targetObject))
        {
            Tween newTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
            activeTweens.Add(newTween);
            //Debug.Log(targetObject.gameObject.name);
            return true;
        }
        else return false;
        //if (activeTween == null)
        //{
        //    //Debug.Log(targetObject.name);
        //    activeTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
        //    }
    }
    private void Update()
    {
        if (activeTweens.Count > 0)
        {
            for (int i = 0; i < activeTweens.Count; i++)
                if (activeTweens[i] != null)
            {
                //Debug.Log("Tweener - ActiveTween: " + i + " " + activeTweens[i].Target.name);
                //Debug.Log("Tweener - ActiveTween Endpos: " + activeTweens[i].EndPos);
                if (Vector3.Distance(activeTweens[i].Target.position, activeTweens[i].EndPos) > 0.1f)
                {
                    float timeFraction = (Time.time - activeTweens[i].StartTime) / activeTweens[i].Duration;
                    timeFraction = timeFraction * timeFraction * timeFraction;
                    activeTweens[i].Target.position = Vector3.Lerp(activeTweens[i].StartPos, activeTweens[i].EndPos, timeFraction);
                }
                else
                {
                    activeTweens[i].Target.position = activeTweens[i].EndPos;
                        //activeTweens[i] = null;
                        activeTweens.RemoveAt(i);
                        //Debug.Log("==============" + i);
                }
            }
        }
        
        
 
    }
    //Math.easeInCubic = private function(float t, Vector3 b, Vector3 c, float d)
    //{
    //    t /= d;
    //    return c * t * t * t + b;
    //}
}
