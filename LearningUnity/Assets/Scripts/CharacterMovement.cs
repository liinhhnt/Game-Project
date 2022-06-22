using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Vector3 movement;
    private float movementSqrMagnitude;
    public float walkSpeed = 1.5f;
    public Animator _animator;
    public AudioSource footsteepSource;
    public AudioClip[] footsteepClips;
    public AudioSource backgroundMusic;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        //footsteepSource = GetComponent<AudioSource>();
    }
    void GetMovementInput()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        Vector3.ClampMagnitude(movement, 1.0f);
        movementSqrMagnitude = movement.sqrMagnitude;
        //Debug.Log("Movement vector: " + movement);
        //Debug.Log("Movement Sqr Magnitude: " + movementSqrMagnitude);
    }
    void CharacterPosition()
    {
        transform.Translate(movement * walkSpeed * Time.deltaTime, Space.World);
    }
    void CharacterRotation()
    {
        Quaternion.LookRotation(movement);
    }
    void WalkingAnimation()
    {
        ////AnimatorControllerParameter moveSpeed = _animator.GetParameter(0);
        //UnityEngine.AnimatorControllerParameter moveSpeed = _animator.GetParameter(0);
        //moveSpeed.defaultFloat = movementSqrMagnitude;
        ////Debug.Log(moveSpeed.defaultFloat);
    }
    void FootsteepAudio()
    {
        if (movementSqrMagnitude > 0.3f && !footsteepSource.isPlaying)
        {
            footsteepSource.clip = footsteepClips[1];
            footsteepSource.volume = movementSqrMagnitude;
            footsteepSource.Play();
            if (backgroundMusic != null) backgroundMusic.volume = 0.5f;
        }
        else if (movementSqrMagnitude <= 0.3f && footsteepSource.isPlaying)
        {
            footsteepSource.Pause();
            if (backgroundMusic != null)  backgroundMusic.volume = 1.0f;
        }
        //Debug.Log(backgroundMusic.volume);
    }

    private void Update()
    {
        GetMovementInput();
        CharacterPosition();
        CharacterRotation();
        WalkingAnimation();
        FootsteepAudio();
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
    }
}
