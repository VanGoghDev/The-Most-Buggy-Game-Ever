using Models.Interfaces;
using UnityEngine;

namespace Models
{
    public class PlayableEntity: MonoBehaviour, IPlayableEntity
    {
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

        #endregion
        
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
            if (IsGrounded() && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown("joystick button 0")))
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

        public void Flip()
        {
            facingRight = !facingRight;
            transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
        }
    }
}