using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    private Rigidbody2D PlayerGravitation;
    private BoxCollider2D Collision;

    public LayerMask WhatIsGround;
    public Transform GroundDetector;
    public float DiameterCheck;
    public float PlayerSpeed;

    public LayerMask WhatIsCable;
    public Transform CableDetector;
    public float climbSpeed;

    public LayerMask WhatIsRod;
    public Transform RodDetector;

    public Vector3 goal1;
    public Vector3 goal2;
    public static int movement = 0;
    private float climbingDownSpeed = 20f;

    public static float CurrentScore = 0f;

    Rigidbody2D rb;

    [SerializeField] private Animator animator; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerGravitation = GetComponent<Rigidbody2D>();
        Collision = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        
        bool canJump = Physics2D.OverlapCircle(GroundDetector.position, DiameterCheck, WhatIsGround);

        bool canClimbCable = Physics2D.OverlapCircle(CableDetector.position, DiameterCheck, WhatIsCable);

        bool canClimbRod = Physics2D.OverlapCircle(RodDetector.position, DiameterCheck, WhatIsRod);

        bool isJumping = (Keyboard.current.spaceKey.isPressed || Keyboard.current.upArrowKey.isPressed);

        animator.SetFloat("yVelocity", rb.linearVelocity.y);

        animator.SetBool("IsGrounded", canJump);

        if (canClimbCable|| canClimbRod)
        {
            
            animator.SetBool("IsClimbing", true);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsJumping", false);

            if (Keyboard.current.upArrowKey.isPressed|| Keyboard.current.spaceKey.isPressed || Keyboard.current.downArrowKey.isPressed) 
            {
                animator.speed = 1;
            }
            else
            {
                animator.speed = 0;
            }
        }
        else
        {
            animator.speed = 1;
            animator.SetBool("IsClimbing", false);

            if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.rightArrowKey.isPressed || Keyboard.current.leftArrowKey.isPressed || Keyboard.current.spaceKey.isPressed)
            {
                if (canJump == true)
                {
                    animator.SetBool("IsRunning", true);
                    animator.SetBool("IsJumping", false);

                    if (transform.position.y < 5)
                    {
                        AudioManager.Instance.PlayGrassWalkSound();
                    }
                    else
                    {
                        AudioManager.Instance.PlayNonGrassWalkSound();
                    }

                }
                else
                {
                    animator.SetBool("IsRunning", false);
                    animator.SetBool("IsJumping", true);
                }
            }
            else
            {
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsRunning", false);
            }
        }

        if (isJumping == true && canJump == true && movement==0)
        {
            PlayerGravitation.linearVelocityY = PlayerSpeed ;
            AudioManager.Instance.PlayjumpSound();

        }

        if (Keyboard.current.rightArrowKey.isPressed && movement == 0)
        {
            
            PlayerGravitation.AddForce(new Vector2(1, 0));

            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Keyboard.current.leftArrowKey.isPressed && movement == 0)
        {


            PlayerGravitation.AddForce(new Vector2(-1, 0));

            transform.localScale = new Vector3(-1, 1, 1);
        }


        if ( canClimbCable == true) 
        {
            PlayerGravitation.gravityScale = 0f;
            PlayerGravitation.linearVelocityY = 0;
            PlayerGravitation.linearVelocityX = 0;
  

            if (Keyboard.current.upArrowKey.isPressed) { PlayerGravitation.AddForce(new Vector2(0, 10)); }
            else if(Keyboard.current.downArrowKey.isPressed) { PlayerGravitation.AddForce(new Vector2(0, -10)); }

            }else { PlayerGravitation.gravityScale = 1f; }

        if (canClimbRod == true)
        {
            PlayerGravitation.gravityScale = 0f;
            PlayerGravitation.linearVelocityY = 0;
            PlayerGravitation.linearVelocityX = 0;

            if (Keyboard.current.upArrowKey.isPressed) { PlayerGravitation.AddForce(new Vector2(0, 30)); }
            else if (Keyboard.current.downArrowKey.isPressed) { PlayerGravitation.gravityScale = 1f; }

        }


        if (movement==1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            Vector3 goal1 = new Vector3(PolesGeneration.GeneralX + 12.2f - 50f, PolesGeneration.height+2f-100f, 0);
            animator.SetBool("IsRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, goal1, 5f * Time.deltaTime);


            if (Vector3.Distance(transform.position, goal1) < 1f)
            {
                movement = 2;
                animator.SetBool("IsRunning", false);
            }
            
        }
        else if (movement == 2)
        {
            animator.speed = 0;
            Vector3 goal2 = new Vector3(PolesGeneration.GeneralX + 12.2f - 50f, 3f, 0);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsClimbing", true);
            transform.position = Vector3.MoveTowards(transform.position, goal2, climbingDownSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, goal2) < 0.1f)
            {
                movement = 3;
            }
        }
        else if (movement == 3)
        {
            animator.speed = 1;
            Vector3 goal3 = new Vector3(PolesGeneration.GeneralX, 3f, 0);
            animator.SetBool("IsRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, goal3, 10f * Time.deltaTime);

            if (Vector3.Distance(transform.position, goal3) < 0.1f)
            {
                movement = 0;
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.CompareTag("Fusebox"))
        {
            climbingDownSpeed += 2;
            movement = 1;
            CurrentScore=CurrentScore+100;
            ScoreLogic.lastAddition = 0;

        }
    }

}
