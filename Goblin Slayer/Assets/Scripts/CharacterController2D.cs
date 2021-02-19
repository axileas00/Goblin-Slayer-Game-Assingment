using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer mySpriteRenderer;
    
    private bool isGrounded;
    public Transform feetPos;
    public float jumpForce;
    public float checkradius;
    public LayerMask whatIsGround;
    public bool playerCanJump = true;
    private float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;
    private float moveInput;
    public float speed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        //GroundChecks - JumpForce
        #region GroundChecks-JumpForce
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkradius, whatIsGround);
        
        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        #endregion  
    }

    private void FixedUpdate()
    {       
        PlayerMovement();
    }
        
    private void PlayerMovement() //PlayerMovement-Animations
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.D)))
        {
            animator.SetBool("IsRunning", true);
            if (Input.GetKey(KeyCode.A))
            {
                // flip the sprite
                mySpriteRenderer.flipX = false;
            }
            else //(Input.GetKey(KeyCode.D))
            {               
                // flip the sprite
                mySpriteRenderer.flipX = true;

            }         
            

        }
        else
        {
            animator.SetBool("IsRunning", false);
        }       
    }
    
}
