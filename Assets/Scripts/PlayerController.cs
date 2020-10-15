using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public int MaxHealt;
    public float TimeInvincible;
    public float JumpForce;
    public LayerMask groundLayer; // Insert the layer here.

    private Rigidbody2D _rigidbody2D;
    private float _horizontal;
    private float _vertical;
    private float _jump;
    private int CurrentHealth;
    private BoxCollider2D boxCollider2D;

    private float _jumpTimeCounter;
    private bool _isJumping = false;
    public float jumpTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = transform.GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealt;
        boxCollider2D = transform.GetComponent<BoxCollider2D>();

        JumpForce = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _jump = Input.GetAxis("Jump");

        Jump();
    }

    private void FixedUpdate()
    {
        _horizontal = Input.GetAxis("Horizontal");
        MoveHorizontal();
    }

    private void Jump()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, JumpForce);
            _isJumping = true;
            _jumpTimeCounter = jumpTime;
        }

        if (Input.GetKey(KeyCode.Space) && _isJumping)
        {
            if (_jumpTimeCounter > 0)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, JumpForce + 3f);
                _jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                _isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isJumping = false;
        }
    }

    private void MoveHorizontal()
    {
        _rigidbody2D.velocity = new Vector2(Speed * _horizontal, _rigidbody2D.velocity.y);
    }

    /// <summary>
    /// If player grounded, returns true
    /// </summary>
    /// <returns></returns>
    private bool IsGrounded()
    {
        float extraHeightText = 0.05f;
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, extraHeightText, groundLayer);
        Color rayColor;
        if (raycastHit2D.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * (boxCollider2D.bounds.extents.y + extraHeightText), rayColor);
        Debug.Log(raycastHit2D.collider != null);
        return raycastHit2D.collider != null;
    }
}
