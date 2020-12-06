using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//On Mini
public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.35f;    //Speed for the player
    [HideInInspector] public bool canMove = true;
    [SerializeField] private Vector2 InputDirection;
    private Animator animator;
    //  For The Smooth Movement Method
    //private Vector3 m_Velocity = Vector3.zero;
    //[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement

    // To calc speed using transform
    public Vector2 m_speed;      //Speed the player is moving at a particular time.
    private Vector3 lastPosition = Vector3.zero;

    // For Multiplayer...
    PhotonView PV;

    //Joystick movementJoystick;
    Rigidbody2D rb;
    private bool facingRight = true;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        //movementJoystick = GameObject.FindGameObjectWithTag("Movement Joystick").GetComponent<FixedJoystick>();
        StartCoroutine(CalculateSpeed());
    }

    // Lerp Speed 3.5 - 4.5 works best, yet
    // new best fixed speed (moveTowards speed = 4)


    private void Update()
    {
        // To give depth to the players or else a player behind another can appear on top of the front one.
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y / 3f);

        // Accept Input if this is a local player
        if (PV.IsMine)    //Comment this line if you want to test player movement.
            InputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        animator.SetFloat("speed", m_speed.magnitude);


        #region +++++++++++++++++++ FOR TESTING SCENE +++++++++++++++++++
        // Comment everything else in this function if you are testing.

        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y / 3f);
        //InputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //animator.SetFloat("speed", InputDirection.magnitude);

        # endregion +++++++++++++++++++ FOR TESTING SCENE +++++++++++++++++++
    }

    void FixedUpdate()
    {
        MoveCharacter(InputDirection, speed);


        ////// For player turning...

        /////**
        ////1)  You can also use InputDirection.x here but just remember to Synchronize Size because then the remote player is
        ////    changing the scale from their side and you have to sync it.
        ////    The direction variable(of other player) in yr instance will not change it will be 0.
        ////2)  If u are using m_speed then you dont need to Synchronize Scale. 
        ////    This works perfectly but sometimes(very rarely) the players facing dir wont be synced after stopping.
        ////3)  So instead i am using both InputDirection for local player and m_speed for remote player.
        ////**/
        if (PV.IsMine)
        {
            //CanMove - because when venting player should not flip character.
            if (((InputDirection.x < 0 && facingRight) || (InputDirection.x > 0 && !facingRight)) && canMove)
                Flip();
        }
        else
        {
            if (((m_speed.x < 0.5 && facingRight) || (m_speed.x > 0.5 && !facingRight)) && Mathf.Approximately(m_speed.x, 0f))
                Flip();
        }


        #region +++++++++++++++++++ FOR TESTING SCENE +++++++++++++++++++
        // Comment everything else in this function if you are testing.

        //MoveCharacter(InputDirection, speed);
        //if (((InputDirection.x < 0 && facingRight) || (InputDirection.x > 0 && !facingRight)) && canMove)
        //    Flip();

        # endregion +++++++++++++++++++ FOR TESTING SCENE +++++++++++++++++++

    }

    public void MoveCharacter(Vector2 direction, float _speed)
    {
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


        // I think this was the method which actual among us game used.
        if (canMove)
            rb.velocity = direction.normalized * _speed;
        else
            rb.velocity = Vector2.zero;

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
            m_speed = (transform.position - lastPosition) / Time.fixedDeltaTime;
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}