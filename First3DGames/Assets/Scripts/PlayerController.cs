using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody myBody;
    private Animator anim;

    public float raycast_length;

    public Transform transform_cylinder;
    public float angle;
    public float followMouseSpeed;
    public FixedJoystick variableJoystick;

    public GameObject bullet;
    public Transform spawnPosition;
    public float bulletForce;

    private Vector3 lastV;
    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

    }
    void Start()
    {

    }


    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward * raycast_length, Color.red);
        //Move();
        //Rotation(); 
        GetJoystickValue();
        Shoot();

    }
    private void GetJoystickValue()
    {
        //Vector3 vec_left = Vector3.zero;
        //vec_left.x = variableJoystick.Direction.x;
        //vec_left.y = variableJoystick.Direction.y;

        //Debug.Log(Time.deltaTime);
        //Debug.LogError(v * Time.deltaTime);
        //Debug.Log(variableJoystick.Direction);
        Vector3 v = new Vector3(variableJoystick.Direction.x, 0f, variableJoystick.Direction.y);// * 5f;
        //Debug.Log(v);
        //transform_cylinder.Translate(v);
        if (v != Vector3.zero) lastV = v;
        if (v != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(v);
            anim.SetBool("Running", true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("RunningUpper", false);
                anim.SetBool("Shooting", true);
                //Debug.Log("Shooting");
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                anim.SetBool("Shooting", false);
                anim.SetBool("RunningUpper", true);
                //Debug.Log("Stop Shooting");
            }
        }
        else
        {
            anim.SetBool("Running", false);
            anim.SetBool("Shooting", false);
            anim.SetBool("RunningUpper", false);
        }
        myBody.velocity = new Vector3(variableJoystick.Direction.x * 5, myBody.velocity.y, variableJoystick.Direction.y * 5);
        transform.rotation = Quaternion.LookRotation(lastV);

    }

    private void Shoot()
    {
        // note phat, hom nao len sua l?i cái ch? h??ng b?n sau
        Debug.DrawRay(spawnPosition.position, transform.forward * raycast_length, Color.red);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Shoot");
            anim.SetBool("Shooting", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("Shooting", false);
        }
    }

    public void SpawnBullet()
    {
        GameObject spawnedBullet = Instantiate(bullet);
        spawnedBullet.transform.position = spawnPosition.position;
        spawnedBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce);
        Destroy(spawnedBullet, 0.5f);
       
    }

}


//private void Move()
//{
//    //left-right
//    Vector3 vec_left = Vector3.zero;
//    vec_left.x = Input.GetAxis("Horizontal");
//    Vector3 v = new Vector3(vec_left.x, 0f, 0f) * Time.deltaTime * 5f;
//    transform_cylinder.Translate(v, Space.Self);

//    //forward-backward
//    Vector3 vec_forward = Vector3.zero;
//    vec_forward.z = Input.GetAxis("Vertical");
//    Vector3 u = new Vector3(0f, 0f, vec_forward.z) * Time.deltaTime * 5f;
//    transform_cylinder.Translate(u, Space.Self);
//}

//private void Rotation()
//{
//    angle += Input.GetAxis("Mouse X") * followMouseSpeed * -Time.deltaTime;
//    angle = Mathf.Clamp(angle, 0, 100);
//    transform_cylinder.localRotation = Quaternion.AngleAxis(angle, Vector3.up);

//}


//    float forceX = 0f;
//    float forceY = 0f;
//    float forceZ = 0f;
//    float velx = Mathf.Abs(myBody.velocity.x);
//    float velz = Mathf.Abs(myBody.velocity.z);
//    float x = Input.GetAxisRaw("Horizontal");
//        if (x > 0)
//        {
//            if (velx<maxVelocity)
//            {
//                forceX = moveForce;
//            }
//            //Vector3 scale = transform.localScale;
//            //scale.x = 1f;
//            //transform.localScale = scale;

//            //anim.SetBool("Walk", true);
//        }
//        else if (x < 0)
//{
//    if (velx < maxVelocity)
//    {
//        forceX = -moveForce;
//    }
//    //Vector3 scale = transform.localScale;
//    //scale.x = -1f;
//    //transform.localScale = scale;
//    //anim.SetBool("Walk", true);
//}
//else if (x == 0)
//{
//    //anim.SetBool("Walk", false);
//}

///*float z = Input.GetAxisRaw("Vertical");
//if (z > 0)
//{
//    if (velz < maxVelocity) { forceZ = moveForce; }
//}
//else if (z < 0)
//{
//    if (velz < maxVelocity) forceZ = -moveForce;
//}*/
//myBody.AddForce(new Vector3(forceX, forceY, forceZ));
//        //else speed = 10;
//        //if (x != 0) myBody.velocity = new Vector3(myBody.velocity.x + speed, myBody.velocity.y, myBody.velocity.z);
//        /*Vector3 vel = Vector3.zero;
//        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
//        {
//            vel.x -= speed;
//        }
//        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
//        {
//            vel.x += speed;
//        }
//        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
//        {
//            vel.z -= speed;
//        }
//        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
//        {
//            vel.z += speed;
//        }

//        myBody.position += vel;*/


//    }