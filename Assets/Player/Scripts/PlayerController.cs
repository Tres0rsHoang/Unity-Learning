using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int movementInitSpeed;
    [SerializeField] int movementMaxSpeed;
    [SerializeField] float collisionOffset = 0.0f;
    [SerializeField] AttackHitBox attackHitBox;
    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    bool canMove = true;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator.SetFloat("xInput", 0);
        animator.SetFloat("yInput", 0);
    }

    // Update is called with time
    private void FixedUpdate() {
        if (canMove && movementInput != Vector2.zero) {
            animator.SetFloat("xInput", movementInput.x);
            animator.SetFloat("yInput", movementInput.y);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * movementInitSpeed * Time.deltaTime), movementMaxSpeed);

            if (movementInput.x > 0) {
                spriteRenderer.flipX = false;
            }
            else if (movementInput.x < 0) {
                spriteRenderer.flipX = true;
            }
            animator.SetBool("isWalking", true);
            
            setAttackHitboxDirrection();
        }
        else {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.9f);
            animator.SetBool("isWalking", false);
        }
    }

    void setAttackHitboxDirrection() {
        float xInput = animator.GetFloat("xInput");
        float yInput = animator.GetFloat("yInput");

        if (xInput == -1 && attackHitBox.attackDirection != AttackHitBox.AttackDirection.left) {
            attackHitBox.attackDirection = AttackHitBox.AttackDirection.left;
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.9f);
        }
        if (xInput == 1 && attackHitBox.attackDirection != AttackHitBox.AttackDirection.right) {
            attackHitBox.attackDirection = AttackHitBox.AttackDirection.right;
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.9f);
        }
        if (yInput == 1 && attackHitBox.attackDirection != AttackHitBox.AttackDirection.up) {
            attackHitBox.attackDirection = AttackHitBox.AttackDirection.up;
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.9f);
        }
        if (yInput == -1 && attackHitBox.attackDirection != AttackHitBox.AttackDirection.down){
            attackHitBox.attackDirection = AttackHitBox.AttackDirection.down;
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.9f);
        } 

    }

    void OnMove(InputValue inputValue) {
        movementInput = inputValue.Get<Vector2>();
    }

    void OnFire() {
        animator.SetTrigger("attack");
        attackHitBox.Attack();
    }

    void lockMovement() {
        canMove = false;
    }

    void unlockMovement() {        
        canMove = true;
    }
}
