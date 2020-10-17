using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class PlayerController : PlayableEntity
{
    Abilities _abilities;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _abilities = GetComponent<Abilities>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        base.Update();
    }

    void FixedUpdate()
    {
        base.FixedUpdate();
    }
    
    public void SetJumpAbility(bool statement, float jumpForceFromTrigger, float jumpTimeFromTrigger)
    {
        _abilities.JumpAbility = statement;
        jumpSpeed = jumpForceFromTrigger;
        jumpDelay = jumpTimeFromTrigger;
    }
}
