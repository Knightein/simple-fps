using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    [Header("Destruction Settings")]
    public int projectilesUntilDestroyed = 5;
    
    private int _counter;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _counter++;
            
            if (_counter >= projectilesUntilDestroyed)
            {
                Destroy(gameObject);
                _counter = 0;
            }
        }
    }
}