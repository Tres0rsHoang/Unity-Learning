using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public enum AttackDirection {
        right, left, up, down
    }
    BoxCollider2D attackCollider;
    public AttackDirection attackDirection;

    public float knowbackFore = 500f;
    public float damage = 1;
    Vector3 parentPosition;
    // Start is called before the first frame update
    void Start() {
        attackCollider = GetComponent<BoxCollider2D>();
        attackCollider.enabled = false;
        parentPosition = GetComponentInParent<Transform>().localPosition;
    }

    void FixedUpdate() {
        parentPosition = GetComponentInParent<Transform>().localPosition;
        Debug.Log("Parent: " +parentPosition +"/n" + "Hitbox: " + transform.position);
    }

    public void Attack() {
        
        switch (attackDirection) {
            case AttackDirection.left: {
                attackCollider.size = new Vector2(0.89f, 1.44f);    
                transform.position = new Vector3(parentPosition.x-0.704f, parentPosition.y-0.598f, parentPosition.z);
                attackCollider.enabled = true;
                break;
            }
            case AttackDirection.right: {
                attackCollider.size = new Vector2(0.89f, 1.44f);
                transform.position = new Vector3(parentPosition.x+0.704f, parentPosition.y-0.598f, parentPosition.z);
                attackCollider.enabled = true;
                break;
            }
            case AttackDirection.up: {
                attackCollider.size = new Vector2(1.21f, 0.51f);
                transform.position = new Vector3(parentPosition.x+0f, parentPosition.y+0f, parentPosition.z);
                attackCollider.enabled = true;
                break;
            }
            case AttackDirection.down: {
                attackCollider.size = new Vector2(1.21f, 0.51f);
                transform.position = new Vector3(parentPosition.x+0.138f, parentPosition.y-1.3f, parentPosition.z);
                attackCollider.enabled = true;
                break;
            }
        }
    }

    public void StopAttack() {
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
           IDamageable enemy = other.GetComponent<IDamageable>();
           if (enemy != null) {
               Vector3 parentPossion = gameObject.GetComponentInParent<Transform>().position;
               
               Vector2 direction = (Vector2) (other.gameObject.transform.position - parentPossion).normalized;
               Vector2 knowback = direction * knowbackFore;

               enemy.TakeDamage(damage, knowback);
           }
           else Debug.LogWarning("Collider does not impletment IDamageable");
        }
    }

}
