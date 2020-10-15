using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;

public class PlayerController : PlayableEntity
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = transform.GetComponent<Rigidbody2D>();
        BoxCollider2D = transform.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Jump();
    }

    private void FixedUpdate()
    {
        Horizontal = Input.GetAxis("Horizontal");
        MoveHorizontal();
    }

    
}
