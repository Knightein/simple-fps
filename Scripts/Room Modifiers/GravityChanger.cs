using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GravityChanger : MonoBehaviour
{
    [Header("Gravity Options")]
    public float gravityMultiplier = 1.5f;
    
    private GameObject _object;
    private GameObject _player;
    private float _originalMass;
    private double _newMass;
    
    // Change the mass of the object to 50% more of its original mass when entering the trigger.
    private void OnTriggerEnter(Collider other)
    {
        _object = other.gameObject;
        
        // Get the parent object if the object doesn't have a rigidbody. This is mainly for Players and AI. 
        if (!_object.GetComponent<Rigidbody>())
        {
            Rigidbody rigidbodyParent = _object.GetComponentInParent<Rigidbody>();
            _object = rigidbodyParent.gameObject;
        }
        
        var originalMass = _object.GetComponent<Rigidbody>().mass;
        var newMass = originalMass * gravityMultiplier;
        
        _object.GetComponent<Rigidbody>().mass = newMass;
    }
    
    // Reset the mass of the object to its original mass after exiting the trigger.
    private void OnTriggerExit(Collider other)
    {
        _object = other.gameObject;

        if (!_object.GetComponent<Rigidbody>())
        {
            Rigidbody rigidbodyParent = _object.GetComponentInParent<Rigidbody>();
            _object = rigidbodyParent.gameObject;
            if (_object.CompareTag("Player"))
            {
                _originalMass = _object.GetComponent<PlayerMovement>().originalMass;
            }
        }
        _object.GetComponent<Rigidbody>().mass = _originalMass;
    }
}