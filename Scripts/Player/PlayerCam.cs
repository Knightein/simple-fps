using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Camera Sensitivity")]
    public float sensX;
    public float sensY;
    
    [Header("Camera Orientation")]
    public Transform orientation;
    
    float _xRotation;
    float _yRotation;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }
    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;
        
        _yRotation += mouseX;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f); // Prevents camera from rotating too far up or down
        
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
    }
}
