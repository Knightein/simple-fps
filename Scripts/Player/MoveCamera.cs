using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Header("Camera Position")]
    public Transform cameraPosition;
    
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
