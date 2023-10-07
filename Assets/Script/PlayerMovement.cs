using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D rb; 
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;

    [SerializeField] private int movingSpeed = 7;
    [SerializeField] private int JumpForce = 14;

    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState {idle, run, jump, fall};
    // Start is called before the first frame update
    void Start() {
        this.rb = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
        this.sprite = GetComponent<SpriteRenderer>();
        this.coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal")*movingSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }

        UpdateAnimation();
    }

    void UpdateAnimation() {
        MovementState state = MovementState.idle;

        if (Input.GetAxisRaw("Horizontal") > 0){
            state = MovementState.run;
            sprite.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) {
            state = MovementState.run;
            sprite.flipX = true;
        }
        if (rb.velocity.y > .1f) {
            state = MovementState.jump;
        }
        else if (rb.velocity.y < -.1f) {
            state = MovementState.fall;
        }
        
        

        anim.SetInteger("state", (int)state);
    }

    bool IsGrounded() {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

}
