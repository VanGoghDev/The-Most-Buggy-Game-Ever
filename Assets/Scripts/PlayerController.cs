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
    private bool dashPressed { get; set; }
    
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
        Rigidbody2D.velocity = new Vector2(Horizontal * speed, Rigidbody2D.velocity.y);
    }

    public void Update()
    {
        if (Horizontal > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else if (Horizontal < 0)
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
        {
            IsJumping = false;
        }
        
    }

    /// <summary>
    /// Move right or left
    /// </summary>
    public void MoveHorizontal()
    {
        if ((Horizontal > 0 && !facingRight) || (Horizontal < 0 && facingRight)) {
            //Flip();
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
}
