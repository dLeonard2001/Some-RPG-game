using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    private PlayerControlMap playerControls;


    public static InputManager getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        playerControls = new PlayerControlMap();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool HasAttack()
    {
        return playerControls.Player.Attack.WasPressedThisFrame();
    }

    public Vector2 GetMovement()
    {
        return playerControls.Player.Move.ReadValue<Vector2>();
    }

    public bool HasJump()
    {
        return playerControls.Player.Jump.WasPressedThisFrame();
    }

    public bool IsSprinting()
    {
        return playerControls.Player.Sprint.IsPressed();
    }
}
