using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class HealthHealer : MonoBehaviour
{
    [Header("Health Options")]
    [Tooltip("How much health to heal.")]
    public float healthToHeal = 100f;
    public GameObject plus;
    bool _healActive;
    private HealthManager _healthManager;
    public GameObject pointLight;
    
    // When the plus sign is active, the player can heal, otherwise the player cannot heal
    
    void Start()
    {
        _healActive = plus;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // Only the player can heal
        {
            if (_healActive)
            {
                _healthManager = other.transform.parent.GetComponentInChildren<HealthManager>();
                if (_healthManager.health < _healthManager.maxHealth)
                {
                    _healthManager.Heal(healthToHeal);
                    _healActive = false;
                    Destroy(plus);
                    Destroy(pointLight);
                }
            }
        }
    }
}
