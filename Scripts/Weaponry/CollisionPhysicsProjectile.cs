using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionPhysicsProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectile;
    [Tooltip("Damage dealt to health")]
    public int damage;

    private bool _hit = false;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 8) // 8 is the layer used for walls and objects the player can move/push on
        {
            Destroy(projectile);
        }
        else if (other.gameObject.CompareTag("ShootableCube"))
        {
            Destroy(projectile);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var head = other.gameObject.name == "Head";
        var hitboxManager = other.GetComponent<HitboxManager>();
        
        if (hitboxManager == null) // Grab the script if it's on the parent (aka bullet hit the head)
        { hitboxManager = other.transform.GetComponentInParent<HitboxManager>(); }
        
        if (hitboxManager == null || _hit) return;
        _hit = true;
        
        if (hitboxManager.healthManager != null)
        {
            hitboxManager.healthManager.TakeDamage(head? damage * 2 : damage); // Deal double damage if it's a headshot
            Destroy(gameObject);
        }
    }
}