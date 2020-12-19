using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//On Mini
public class PlayerMovement : MonoBehaviour
{
    public float speed = 4.5f;    //Speed of the player
    public float rotSpeed = 0.1f; //Rotation Speed of the player
    [HideInInspector] public bool canMove = true;
    [SerializeField] private Vector2 InputDirection;
    private Animator animator;

    //  For The Smooth Movement Method
    //private Vector3 m_Velocity = Vector3.zero;
    //[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement

    // To calc speed using transform
    public Vector3 m_moveSpeed;      //Speed this player is moving at a particular time.
    private Vector3 lastPosition = Vector3.zero;

    // For Multiplayer...
    PhotonView PV;

    Rigidbody rb;

    void Start()
    {
        rotSpeed = speed / 30;

        PV = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        //movementJoystick = GameObject.FindGameObjectWithTag("Movement Joystick").GetComponent<FixedJoystick>();

        StartCoroutine(CalculateSpeed());
    }

    // Lerp Speed 3.5 - 4.5 works best, yet
    // new best fixed speed (moveTowards speed = 4)


    private void FixedUpdate()
    {
        // Rotate both local and remote Players based on their speed.
        RotatePlayer();

        if (PV.IsMine)
        {
            MoveCharacter(InputDirection, speed);

            // If this is a local player then accept input and change animators parameters.
            InputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // Check for animation State Change based on Input
            if (InputDirection.magnitude != 0)
                animator.SetBool("running", true);
            else
                animator.SetBool("running", false);
        }
        else
        {
            // Check for animation State Change based on speed
            if (m_moveSpeed.magnitude > 0.01f)
                animator.SetBool("running", true);
            else
                animator.SetBool("running", false);
        }
    }

    public void MoveCharacter(Vector2 inputDirection, float _speed)
    {
        #region Other Methods
        //Not my method , gives smooth movement but its not the among us feel
        // Move the character by finding the target velocity
        //Vector3 targetVelocity = movDir * _speed;
        // And then smoothing it out and applying it to the character
        //rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        //My Method, not smooth movement but gives the among us movement feel but its lagging idk why...
        //Vector2 targetPos = rb.position + (direction.normalized * _speed * Time.deltaTime * 10);
        //rb.MovePosition(targetPos);


        //Vector3 desiredPosition = (Vector2)transform.position + (direction.normalized);
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _speed * Time.fixedDeltaTime * 100);

        //if (canMove)
        //    rb.MovePosition(smoothedPosition);
        //else
        //    rb.MovePosition(Vector2.zero);
        #endregion Other Methods

        // I think this was the method which actual among us game used.
        if (canMove)
        {
            Vector3 velocityDir = new Vector3(inputDirection.x, 0f, inputDirection.y);
            rb.velocity = velocityDir.normalized * _speed;
        }
        else
            rb.velocity = Vector3.zero;
    }

    private void RotatePlayer()
    {
        if (m_moveSpeed.magnitude >= 0.2f)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_moveSpeed), rotSpeed);
    }

    IEnumerator CalculateSpeed()
    {
        //Vector3 difference = transform.position - lastPosition;
        //m_speed = (difference) / Time.fixedDeltaTime;
        //lastPosition = transform.position;
        while (Application.isPlaying)
        {
            lastPosition = transform.position;
            yield return new WaitForFixedUpdate();
            m_moveSpeed = (transform.position - lastPosition) / Time.fixedDeltaTime;
        }
    }
}