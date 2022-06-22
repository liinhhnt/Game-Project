using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Animator _animator;
    public bool _isSneakLeft;
    public bool _isFacingLeft;
    public float moveSpeed = 10f;
    private bool isInZone;
    bool moving;
    public LayerMask zoneLayer;

    Vector2 lastClickedPos;

    Vector3 targetPos;

    

    private void Start()
    {
        _isSneakLeft = true;
        _isFacingLeft = true;
    }
    private void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
            //transform.DOMove(new Vector3(lastClickedPos.x, transform.position.y), 2);
            targetPos = new Vector3(lastClickedPos.x, transform.position.y, transform.position.z);
            isInZone = Physics2D.OverlapArea(new Vector2(targetPos.x - 0.25f, targetPos.y - 0.5f), new Vector2(targetPos.x + 0.25f, targetPos.y + 0.5f), zoneLayer);
        }
        if (moving && isInZone && transform.position != targetPos)
        {
            float step = moveSpeed * Time.deltaTime;
            if (targetPos.x < transform.position.x)
            {
                _isSneakLeft = true;
                _animator.SetBool("IsSneakLeft", true);
                _animator.SetBool("IsSneakRight", false);
                //Debug.Log("Go to left");
            }
            else
            {
                _isSneakLeft = false;
                _animator.SetBool("IsSneakLeft", false);
                _animator.SetBool("IsSneakRight", true);
                //Debug.Log("Go to right");

            }


            transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
        }
        else
        {
            Debug.Log("Stop!");
            moving = false;
            _animator.SetBool("IsSneakLeft", false);
            _animator.SetBool("IsSneakRight", false);
        }
    }

    private void OpenDoor()
    {

    }
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //    if (_isSneekLeft)
    //    {
    //        _animator.SetBool("IsSneekLeft", true);
    //    }
    //    else
    //    {
    //        _animator.SetBool("IsSneekLeft", false);
    //    }

    //    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    mousePosition.z += Camera.main.nearClipPlane;

    //    transform.position = Vector2.MoveTowards(transform.position, mousePosition, moveSpeed * Time.deltaTime);
    //    //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    //mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
    //    //transform.position = mousePosition;
    //}
}
