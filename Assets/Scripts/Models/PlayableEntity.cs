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
            
            Rigidbody2D.velocity = new Vector2(speed * Horizontal, Rigidbody2D.velocity.y);
        }

        public void Jump()
        {
            if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            {
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpForce);
                IsJumping = true;
                JumpTimeCounter = jumpTime;
            }

            if (Input.GetKey(KeyCode.Space) && IsJumping)
            {
                if (JumpTimeCounter > 0)
                {
                    Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpForce);
                    JumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    IsJumping = false;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                IsJumping = false;
            }
        }
    }
}