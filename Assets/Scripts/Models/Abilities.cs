using UnityEngine;

namespace Models
{
    /// <summary>
    /// Represents class which contains abilities player (their on/off statement)
    /// </summary>
    public class Abilities : MonoBehaviour
    {
        /// <summary>
        /// Jumping ability
        /// </summary>
        public bool JumpAbility { get; set; }

        public Abilities()
        {
            JumpAbility = false;
        }
    }
}
