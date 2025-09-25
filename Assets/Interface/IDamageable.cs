using UnityEngine;

public interface IDamageable {
    public float Health {set; get;}
    void TakeDamage(float damage, Vector2 knowback);
}