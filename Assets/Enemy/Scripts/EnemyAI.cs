using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IDamageable
{
    [SerializeField] float health;
    Animator animator;
    Rigidbody2D rb;
    [SerializeField] DetectionZone detectionZone;
    [SerializeField] float movementSpeed;

    SpriteRenderer spriteRenderer;

    public float Health {
        set {
            health = value;
            if (health < 0) {
                Defeated();
            }
        }
        get {
            return health;
        }
    }

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        if (detectionZone.detectObjs.Count > 0) {
            Vector2 direction = (detectionZone.detectObjs[0].transform.position - transform.position).normalized;
            rb.AddForce(movementSpeed * direction * Time.deltaTime);
            if (direction.x > 0) {
                spriteRenderer.flipX = false;
            }
            else if (direction.x < 0) {
                spriteRenderer.flipX = true;
            }
            animator.SetBool("isMoving", true);
        }    
    }

    void Defeated() {
        animator.SetTrigger("Defeated");
    }

    void removeEnemy() {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage, Vector2 knowback) {
        animator.SetTrigger("TakeDamage");        
        Health -= damage;
        rb.AddForce(knowback);
    }
}
