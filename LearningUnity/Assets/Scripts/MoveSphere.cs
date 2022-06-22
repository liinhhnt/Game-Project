using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphere : MonoBehaviour
{
    private Vector3 target = new Vector3(-3, 1, 0);
    private float duration = 1.5f;
    public Tweener tweener;

    private void Update()
    {
        //if (!tweener.TweenExists(transform))
        //{
        //    Vector3 tmp = transform.position;
        //    tmp.x *= -1;
        //    transform.position = tmp;
        //    tweener.AddTween(transform, transform.position, target, duration/SpeedManager.speedModifier);
        //}
    }
} 
