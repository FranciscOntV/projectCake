﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerController controller;
    private Vector3 movement;
    private bool running = false;

    void Update()
    {
        this.checkMovement();
        this.checkInteract();
        this.checkJump();
    }

    /// <summary>
    /// checks for interactable objects.
    /// </summary>
    public void checkInteract()
    {
        if (isPressingInteractButton())
        {
            controller.interact();
        }
    }

    /// <summary>
    /// Checks if the character should move.
    /// </summary>
    public void checkMovement()
    {
        running = isPressingRunButton();
        movement = getAxisValues();
        if (movement.z != 0f || movement.x != 0f)
        {
            controller.updateDirection(movement);
        }
        if (controller.canWalkForward())
        {
            controller.moveCharacter(movement, running);
        }
    }

    /// <summary>
    /// Checks if the character should jump.
    /// </summary>
    public void checkJump()
    {
        if (isPressingJumpButton() && controller.canJump())
        {
            controller.jump();
        }
    }

    private void Reset()
    {
        this.controller = GetComponent<PlayerController>();
    }

    // This section must be edited if moving to the new input system
    // https://forum.unity.com/threads/input-system-update.508660/
    private bool isPressingRunButton()
    {
        return Input.GetButton("Fire3");
    }

    private bool isPressingInteractButton()
    {
        return Input.GetButtonDown("Jump");
    }

    private bool isPressingJumpButton()
    {
        return Input.GetKeyDown(KeyCode.J);
    }

    private Vector3 getAxisValues()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
    }
}
