using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private float timeChangeDir;
    private float currentTime;
    private Animator anim;
    private Vector3 moveDir;
    private Rigidbody rb;
    public float speed = 5;
    void Start()
    {
        timeChangeDir = Random.Range(1, 3);
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        moveDir = new Vector3(Random.Range(-1, 1), transform.position.y, Random.Range(-1, 1));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            anim.SetBool("Death_b", true);
            
        }
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
    private void Move()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeChangeDir)
        {
            currentTime = 0;
            timeChangeDir = Random.Range(1, 3);
            moveDir = new Vector3(Random.Range(-1, 1), transform.position.y, Random.Range(-1, 1));
        }
        rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed);
        Vector3 v = new Vector3(moveDir.x, 0f, moveDir.z);
        transform.rotation = Quaternion.LookRotation(v);
    }
}
