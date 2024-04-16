using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] 
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    
    bool _readyToJump;
    
    [Header("Mass Settings")]
    public float originalMass;
    
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool _isGrounded;
    
    [Header("Components")]
    public Transform orientation;

    float _horizontalInput;
    float _verticalInput;
    
    Vector3 _moveDirection;
    
    Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _readyToJump = true;
        _rb.mass = originalMass;
    }
    
    private void Update()
    {
        // Ground Check
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 
            playerHeight * 0.5f + 0.2f, whatIsGround);
        
        MyInput();
        SpeedControl();
        
        // Apply drag if player is on the ground
        _rb.drag = _isGrounded ? groundDrag : 0f;
    }
    
    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    /// <summary>
    /// This method controls the player's input.
    /// </summary>
    private void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        
        if(Input.GetKey(jumpKey) && _isGrounded && _readyToJump)
        {
            Jump();
            _readyToJump = false;
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    
    /// <summary>
    /// This method moves the player.
    /// </summary>
    private void MovePlayer()
    {
        // Moves the player in the direction of the camera
        _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
        
        // Grounded
        if(_isGrounded)
            _rb.AddForce(_moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);
        
        // In Air
        else if(!_isGrounded)
            _rb.AddForce(_moveDirection.normalized * (moveSpeed * 10f * airMultiplier), ForceMode.Force);
    }
    
    /// <summary>
    /// This method limits the player's speed.
    /// </summary>
    private void SpeedControl()
    {
        var velocity = _rb.velocity;
        Vector3 flatVelocity = new Vector3(velocity.x, 0f, velocity.z);

        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            _rb.velocity = new Vector3(limitedVelocity.x, velocity.y, limitedVelocity.z);
        }
    }
    
    /// <summary>
    /// This method makes the player jump.
    /// </summary>
    private void Jump()
    {
        // Must reset y velocity to 0 before applying jump force
        var velocity = _rb.velocity;
        velocity = new Vector3(velocity.x, 0f, velocity.z);
        _rb.velocity = velocity;
        
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    
    /// <summary>
    /// This method resets the jump cooldown.
    /// </summary>
    private void ResetJump()
    {
        _readyToJump = true;
    }
}
