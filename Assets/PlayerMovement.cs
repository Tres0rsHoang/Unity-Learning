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
    private int movingVelocity = 7;
    // Start is called before the first frame update
    void Start() {
        this.rb = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
        this.sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal")*movingVelocity, rb.velocity.y);

        if (Input.GetButtonDown("Jump")) {
            rb.velocity = new Vector2(rb.velocity.x, movingVelocity);
        }

        UpdateAnimation();
    }

    void UpdateAnimation() {
        if (Input.GetAxisRaw("Horizontal") > 0){
            anim.SetBool("isRunning", true);
            sprite.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) {
            anim.SetBool("isRunning", true);
            sprite.flipX = true;
        }
        else {
            anim.SetBool("isRunning", false);
        }
    }
}
