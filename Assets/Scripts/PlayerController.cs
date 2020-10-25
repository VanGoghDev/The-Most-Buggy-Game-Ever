using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public Abilities abilities;
    
    public LayerMask groundLayer;
    protected BoxCollider2D BoxCollider2D { get; set; }
    protected Rigidbody2D Rigidbody2D { get; set; }
    protected float Horizontal { get; set; }
    public float speed;
    private bool facingRight = true;

    #region jumping variables

    public float jumpForce;
    public float JumpTimeCounter { get; set; }
    private bool IsJumping { get; set; }
    public float jumpTime;
    private bool jumpPressed { get; set; }

    private bool dashing = false;

    private float dashTime = 0;

    public float dashSpeed;
    
    #endregion
    private void Start()
    {
        Rigidbody2D = transform.GetComponent<Rigidbody2D>();
        BoxCollider2D = transform.GetComponent<BoxCollider2D>();
        abilities = gameObject.AddComponent<Abilities>();
        abilities.JumpAbility = true;
    }

    public void FixedUpdate()
    {
        Horizontal = Input.GetAxis("Horizontal");
        

        if ((Horizontal > 0 && !facingRight) || (Horizontal < 0 && facingRight)) {
            Flip();
        }
    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashTime = 0.2f;
            dashing = true;
            Dash();
        }

        dashTime -= Time.deltaTime;
        
        if (Horizontal != 0 && dashTime <= 0)
        {
            Rigidbody2D.velocity = new Vector2(Horizontal * speed, Rigidbody2D.velocity.y);
        }
        
        if (Horizontal > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } 
        else if (Horizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        
        if (IsGrounded() && (Input.GetKeyDown(KeyCode.W)))
        {
            //Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpForce);
            IsJumping = true;
            JumpTimeCounter = jumpTime;
            //Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpForce);
        }

        if (Input.GetKey(KeyCode.W) && IsJumping)
        {
            if (JumpTimeCounter > 0)
            {
                //Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpForce);
                //Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpForce);
                
                JumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                IsJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
            IsJumping = false;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
            
    }

    public void Dash()
    {
        // var vSpeed = new Vector2(200, 0);
        // if (!facingRight)
        //     vSpeed = new Vector2(-200, 0);
        //Rigidbody2D.MovePosition(Rigidbody2D.position + vSpeed);
            
        //var dashVector = new Vector2(dashX, dashY);
        var oldGravity = Rigidbody2D.gravityScale;
        Rigidbody2D.gravityScale = 0;
        var vSpeed = new Vector2(dashSpeed, 0);
        var dashVector = Rigidbody2D.position + vSpeed;
        if (!facingRight)
            dashVector *= -1;
        dashVector.y = 0;

        Rigidbody2D.AddForce(dashVector, ForceMode2D.Impulse);
        //Rigidbody2D.velocity = Vector2.left * 20;
        dashing = false;
        Rigidbody2D.gravityScale = oldGravity;
    }
    
    /// <summary>
    /// Move right or left
    /// </summary>
    public void MoveHorizontal()
    {
        if ((Horizontal > 0 && !facingRight) || (Horizontal < 0 && facingRight)) {
            Flip();
        }
        
        Rigidbody2D.velocity = new Vector2(speed * Horizontal, Rigidbody2D.velocity.y);
    }
    
    public void Jump()
    {
        if (IsGrounded() && (jumpPressed))
        {
            //Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpForce);
            Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            IsJumping = true;
            JumpTimeCounter = jumpTime;
        }

        if (Input.GetKey(KeyCode.W) && IsJumping)
        {
            if (JumpTimeCounter > 0)
            {
                //Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpForce);
                Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    
                JumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                IsJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            IsJumping = false;
        }
    }

    public void SetJumpAbility(bool statement, float jumpForceFromTrigger, float jumpTimeFromTrigger)
    {
        abilities.JumpAbility = statement;
        jumpForce = jumpForceFromTrigger;
        jumpTime = jumpTimeFromTrigger;
        Debug.Log(abilities.JumpAbility);
        Debug.Log(jumpTime);
        Debug.Log(jumpForce);
    }
    
    /// <summary>
    /// If player grounded, returns true
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        float extraHeightText = 0.05f;
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(BoxCollider2D.bounds.center, BoxCollider2D.bounds.size, 
            0, Vector2.down, extraHeightText, groundLayer);
        return raycastHit2D.collider != null;
    }
    
    public void Flip()
    {
        facingRight = !facingRight;
    }
}
