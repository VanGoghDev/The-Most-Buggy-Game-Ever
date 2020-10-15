using UnityEngine;

namespace Models.Interfaces
{
    public interface IPlayableEntity
    {
        // LayerMask GroundLayer { get; set; }
        // BoxCollider2D BoxCollider2D { get; set; }
        // Rigidbody2D Rigidbody2D { get; set; }
        // float Horizontal { get; set; }
        // float Speed { get; set; }
        //
        // #region jumping variables
        //
        // float JumpForce { get; set; }
        // float JumpTimeCounter { get; set; }
        // bool IsJumping { get; set; }
        // float JumpTime { get; set; }
        //
        // #endregion
        //
        bool IsGrounded();
        void MoveHorizontal();
        void Jump();
    }
}