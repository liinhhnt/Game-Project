using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour

{
    int level;
    private Rigidbody2D rb;
    private bool isStop = true;
    private float moveSpeed = 75f;
    private int _direction = 1;
    [SerializeField] private float jumpForce = 250f;
    //private bool isJumping;
    private bool isDoubleJump = false;
    private bool isGround = true;
    public bool isDied = false;
    public float addJumpSpeed = 50f;
    private int changeDirection = 1;
    public GameObject gameOver;
    public Animator _animator;
    //public GameObject nextScene;
    //public GameObject currentScene;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        CameraFollowPlayer.instance.player = transform;

        string readValue = "";

        TextAsset jsonText = Resources.Load<TextAsset>("PlayerData");
        readValue = jsonText.text;

        PlayerDataInfo.listPlayerInput = JsonConvert.DeserializeObject<List<PlayerInfo>>(readValue);
        PlayerDataInfo.listPlayerInput.ForEach(x =>
        {
            Debug.Log("id: " + x.id + ",\nmoveSpeed: " + x.moveSpeed + ",\njumpForce: " + x.jumpForce + ",\naddJumpSpeed: " + x.addJumpSpeed);
        });

    }

    void InitCharacter()
    {
        Debug.Log(moveSpeed);
        moveSpeed = PlayerDataInfo.listPlayerInput[level].moveSpeed;
    }

    void Update()
    {
        Move();
        Jump();

        if (isStop)
        {
            _animator.SetBool("IsRunning", false);
        }
        else
        {
            _animator.SetBool("IsRunning", true);
        }

        if (isGround)
        {
            _animator.SetBool("IsJumping", false);
        }
        else
        {
            _animator.SetBool("IsJumping", true);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
        }
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("You are died!");
            StartCoroutine(ResetGame());
            level = 0;
            isDied = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Spike")
        {
            Debug.Log("You are died!");
            isDied = true;
            StartCoroutine(ResetGame());
            level = 0;
            isDied = false;
        }

        if (other.gameObject.tag == "Coin")
        {
            Debug.Log("Boost Speed");
            level++;
            InitCharacter();
        }

        /*if (other.gameObject.tag == "Flag")
        {
            Debug.Log("Next level");
            currentScene.SetActive(false);
            nextScene.SetActive(true);
        }*/
    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //if (_direction == 1) changeDirection = -1;
            //else changeDirection = 1;
            _direction = -1;
            isStop = false;
            if (transform.localScale.x > 0) 
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //if (_direction == -1) changeDirection = -1;
            //else changeDirection = 1;
            _direction = 1;
            isStop = false;
            if (transform.localScale.x < 0) 
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            isStop = true;
        }
        if (!isStop)
        {
            if (isGround)
                rb.velocity = new Vector2(moveSpeed * _direction, rb.velocity.y);
            else
                rb.velocity = new Vector2(_direction * (moveSpeed + addJumpSpeed), rb.velocity.y);
            //transform.localScale = new Vector3(changeDirection * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGround)
            {
                isDoubleJump = false;
                isGround = false;
                rb.velocity = new Vector2(rb.velocity.x + addJumpSpeed, jumpForce);
            }
            else if (!isDoubleJump)
            {
                isDoubleJump = true;
                rb.velocity = new Vector2(rb.velocity.x + addJumpSpeed, jumpForce);
            }
        }
    }


    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(0.1f);
        // hien thi gameover
        gameOver.SetActive(true);
    }
}



//    [SerializeField] private Animator _animator;
//    public Rigidbody2D rb;
//    bool isJumping = false;
//    bool isRunning = false;
//    bool rightDirec = true;
//    public float moveSpeed;
//    private int _direction = 1;
//    bool isGround = true;
//    public float jumpAmount;
//    public bool isDied = false;
//    public GameObject gameOver;
//    public LayerMask groundLayer;
//    private bool canDoubleJump = false;
//    private bool canJumpOnWall = false;
//    void Start()
//    {
//        isDied = false;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //Debug.DrawRay(transform.position, new Vector3(0, -11, 0), Color.red);
//        Move();
//        Jump();
//        //JumpOnWall();
//        /*RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(0, -1, 0), 10, groundLayer);
//        if(hit.collider!=null)
//        {
//            Debug.Log("Grounded");
//            isGround = true;
//            stepJumped = 0;
//        }
//        else
//        {
//            Debug.Log("Flying");
//            isGround = false;
//            stepJumped = 0;
//        }*/
//        if (!isRunning)
//        {
//            _animator.SetBool("IsRunning", false);
//        }
//        else
//        {
//            _animator.SetBool("IsRunning", true);
//        }
//        if (!isJumping)
//        {
//           // _animator.SetBool("IsRunning", true);
//            _animator.SetBool("IsJumping", false);
//        }
//        else
//        {
//           // _animator.SetBool("IsRunning", false);
//            _animator.SetBool("IsJumping", true);
//        }

//    }


//    private void OnCollisionEnter2D(Collision2D other)
//    {
//        //if (other.gameObject.tag == "Wall")
//        //{
//        //    Debug.Log("Crash!");
//        //    _direction *= -1;
//        //    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
//        //}
//        if (other.gameObject.tag == "Ground")
//        {
//            canJumpOnWall = true;
//        }
//        else canJumpOnWall = false;
//        if (other.gameObject.tag == "Enemy")
//        {
//            Debug.Log("You are died!");
//            isDied = true;
//            StartCoroutine(ResetGame());
//            isDied = false;
//        }

//    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.tag == "Spike")
//        {
//            Debug.Log("You are died!");
//            isDied = true;
//            StartCoroutine(ResetGame());
//            isDied = false;
//        }
//    }

//    /*private void Move(int direction)
//{
//    Vector2 moveDirection = transform.position;
//    moveDirection.x += speed * Time.deltaTime * direction;
//    transform.position = moveDirection;
//   // isRunning = true;
//}*/

//    private void Move()
//    {
//        if (Input.GetKey(KeyCode.LeftArrow))
//        {
//            if (transform.localScale.x > 0)
//                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
//            rb.AddForce(-transform.right * moveSpeed);
//            isRunning = true;
//        }
//        if (Input.GetKey(KeyCode.RightArrow))
//        {
//            if(transform.localScale.x < 0)
//                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
//            rb.AddForce(transform.right * moveSpeed);
//            isRunning = true;
//        }

//        //if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
//        //    return;
//        //if (Input.GetKey(KeyCode.LeftArrow))
//        //    _direction = -1;
//        //if (Input.GetKey(KeyCode.RightArrow))
//        //    _direction = 1;

//        //moveSpeed = 100;
//        //isRunning = true;
//        //rb.velocity = new Vector2(moveSpeed * _direction, rb.velocity.y);
//        //transform.localScale = new Vector3(_direction*transform.localScale.x, transform.localScale.y, transform.localScale.z);

//        //if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
//        //{
//        //    moveSpeed = 0;
//        //    isRunning = false;
//        //    rb.velocity = new Vector2(moveSpeed * _direction, rb.velocity.y);
//        //    transform.localScale = new Vector3(_direction * transform.localScale.x, transform.localScale.y, transform.localScale.z);
//        //}
//        //if (Input.GetKey(KeyCode.RightArrow))
//        //{
//        //    isRunning = true;
//        //    //Vector2 moveDirection = transform.position;
//        //    //moveDirection.x += moveSpeed * Time.deltaTime;
//        //    //transform.position = moveDirection;

//        //    if (!rightDirec)
//        //    {
//        //        Vector2 direc = transform.localScale;
//        //        direc.x *= -1;
//        //        transform.localScale = direc;
//        //    }
//        //    rightDirec = true;
//        //}
//        //if (Input.GetKey(KeyCode.LeftArrow))
//        //{
//        //    isRunning = true;
//        //    Vector2 moveDirection = transform.position;
//        //    moveDirection.x += moveSpeed * Time.deltaTime * -1;
//        //    transform.position = moveDirection;
//        //    if (rightDirec)
//        //    {
//        //        Vector2 direc = transform.localScale;
//        //        direc.x *= -1;
//        //        transform.localScale = direc;
//        //    }
//        //    rightDirec = false;
//        //}
//    }
//    /*private void Jump()
//    {
//        if (Input.GetKeyDown(KeyCode.UpArrow))
//        {
//            isJumping = true;
//            //isRunning = false;
//            Debug.Log("Jumping");
//            if (isGround) 
//            {
//                rb.AddForce(new Vector2(0, jumpAmount), ForceMode2D.Impulse);
//                isGround = false;
//            }
//            Vector2 jumpHeight = transform.position;
//            jumpHeight.y += jumpAmount * Time.deltaTime;
//            transform.position = jumpHeight;


//        }
//        if (Input.GetKeyUp(KeyCode.UpArrow))
//        {
//            isJumping = false;
//            //isRunning = false;
//            Debug.Log("Jumping");
//            if (isGround)
//            {
//                rb.AddForce(new Vector2(0, jumpAmount), ForceMode2D.Impulse);
//                isGround = false;
//            }
//            Vector2 jumpHeight = transform.position;
//            jumpHeight.y += jumpAmount * Time.deltaTime;
//            transform.position = jumpHeight;


//        }
//        if (Input.GetKey(KeyCode.DownArrow))
//        {
//            Debug.Log("Jumping");
//            isJumping = false;
//            //isRunning = true;
//        }
//    }*/

//    private void Jump()
//    {
//        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(0, -1, 0), 11, groundLayer);
//        if (hit.collider != null)
//        {
//            Debug.Log("Grounded");
//            isGround = true;
//        }
//        else
//        {
//            Debug.Log("Flying");
//            isGround = false;
//        }
//        if (Input.GetKeyDown(KeyCode.UpArrow))
//        {
//            isJumping = true;
//            if (isGround)
//            {
//                rb.AddForce(new Vector2(0, jumpAmount), ForceMode2D.Impulse);
//                canDoubleJump = true;
//            }
//            else if (canDoubleJump)
//            {
//                jumpAmount /= 1.5f;
//                rb.AddForce(new Vector2(0, jumpAmount), ForceMode2D.Impulse);
//                canDoubleJump = false;
//                jumpAmount *= 1.5f;
//            }
//        }
//    }

//    /*private void JumpOnWall()
//    {
//        if (Input.GetKeyDown(KeyCode.UpArrow))
//        {
//            isJumping = true;
//            if (canJumpOnWall)
//            {
//                Debug.Log("Climbing 1");
//                rb.AddForce(new Vector2(0, jumpAmount), ForceMode2D.Impulse);
//                canDoubleJump = true;
//            }
//            else if (canJumpOnWall)
//            {
//                Debug.Log("Climbing 2");
//                jumpAmount /= 1.5f;
//                rb.AddForce(new Vector2(0, jumpAmount), ForceMode2D.Impulse);
//                canJumpOnWall = false;
//                canDoubleJump = false;
//                jumpAmount *= 1.5f;
//            }
//        }
//    }*/
//    IEnumerator ResetGame()
//    {
//        yield return new WaitForSeconds(0.1f);
//        // hien thi gameover
//        //gameOver.SetActive(true);
//    }
