using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : PlayableEntity
{
    // Start is called before the first frame update
    [HideInInspector]
    public Abilities abilities;

    private void Start()
    {
        Rigidbody2D = transform.GetComponent<Rigidbody2D>();
        BoxCollider2D = transform.GetComponent<BoxCollider2D>();
        abilities = gameObject.AddComponent<Abilities>();
    }

    private void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        if (abilities.JumpAbility)
            Jump();
    }

    private void FixedUpdate()
    {
        Horizontal = Input.GetAxis("Horizontal");
        MoveHorizontal();
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
}
