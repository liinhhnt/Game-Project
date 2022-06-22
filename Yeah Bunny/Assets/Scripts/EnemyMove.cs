using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private int _direction;
    public float speed;
    public LayerMask wallLayer;
    void Start()
    {
        _direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, new Vector3(6*_direction, 0, 0), Color.blue);
        Move(_direction);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(_direction, 0, 0), 6, wallLayer);
        if (hit.collider != null)
        {
           // Debug.Log("Hit the wall");
            _direction *= -1;
        }
    }

    private void Move(int direction)
    {
        Vector2 moveDirection = transform.position;
        moveDirection.x += speed * Time.deltaTime * direction;
        transform.position = moveDirection;
    }

  
}

