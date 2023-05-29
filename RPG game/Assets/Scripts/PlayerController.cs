using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float ms;
    [SerializeField] private float sprintMS;
    [SerializeField] private int maxMS;
    [SerializeField] private float jumpForce;
    [SerializeField] private float slideForce;
    private bool _HasJumped;
    private bool _IsSprinting;

    private Vector2 _movement;
    private Vector2 _mouseDelta;
    private Vector3 _forceToAdd;

    [Header("Camera Settings")] 
    [SerializeField] private Camera mainCam;
    [SerializeField] [Range(0.1f, 10f)] private float xSensitivity;
    [SerializeField] [Range(0.1f, 10f)] private float ySensitivity;
    private float yRotation;

    [Header("Cached References")] 
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform t;
    [SerializeField] private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        t = transform;
        rb = GetComponent<Rigidbody>();
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        mainCam = Camera.main;

        inputManager = InputManager.getInstance();
    }
    
    // input related
    void Update()
    {
        _movement = inputManager.GetMovement();
        _mouseDelta = inputManager.GetMouseDelta();

       _HasJumped = inputManager.HasJump();
       _IsSprinting = inputManager.IsSprinting();
       
       // Debug.Log($"{_movement} : {_mouseDelta}");
    }

    // physics related, mostly
    private void FixedUpdate()
    {
        HandleMovement();
    }

    // camera related, mostly
    private void LateUpdate()
    {
        
    }

    // function(s) to handle all movement
    #region Movement

    private void HandleMovement()
    {

        _forceToAdd = t.forward * _movement.y + t.right * _movement.x;

        if (_HasJumped)
            _forceToAdd += new Vector3(0, jumpForce, 0);

        _forceToAdd = _IsSprinting ? _forceToAdd.normalized * sprintMS : _forceToAdd.normalized * ms;

        rb.AddForce(_forceToAdd, ForceMode.Impulse);
        
        if (rb.velocity.magnitude > maxMS)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxMS);
        }
    }
    

    #endregion

    
}
