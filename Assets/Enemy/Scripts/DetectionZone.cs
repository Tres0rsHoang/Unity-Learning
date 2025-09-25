using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectObjs = new List<Collider2D>();

    [SerializeField] Collider2D col;

    private void OnTriggerEnter2D(Collider2D collider) { 
        if (collider.tag == "Player") detectObjs.Add(collider);
    }

    private void OnTriggerExit2D(Collider2D collider) {
        detectObjs.Remove(collider);
    }
}
