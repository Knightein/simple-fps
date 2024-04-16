using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitboxManager : MonoBehaviour
{
    [Header("Hitbox Options")]
    public BoxCollider hitbox;
    public HealthManager healthManager;
    
    private EnemyAI _enemyAI;
    private static bool _locked;
    private GameObject _object;
    private CollisionPhysicsProjectile _collisionPhysicsProjectile;
    
    void Start()
    {
        hitbox = GetComponent<BoxCollider>();
        _enemyAI = transform.parent.GetComponent<EnemyAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _object = other.gameObject;
        _collisionPhysicsProjectile = other.GetComponent<CollisionPhysicsProjectile>();
        if (_object.gameObject.CompareTag("Bullet"))
        {
            Destroy(_object);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _object = other.gameObject;
        if (_object.gameObject.CompareTag("Bullet"))
        {
            Destroy(_object);
        }
    }
}
