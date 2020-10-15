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

    void Start()
    {
        Rigidbody2D = transform.GetComponent<Rigidbody2D>();
        BoxCollider2D = transform.GetComponent<BoxCollider2D>();
        abilities = gameObject.AddComponent<Abilities>();
    }

    void Update()
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

    public void SetJumpAbility(bool statement)
    {
        abilities.JumpAbility = statement;
    }
}
