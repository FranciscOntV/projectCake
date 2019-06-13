using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Range(1f, 3f)]
    public float moveSpeed = 1f;
    public PlayerController controller;

    private const float movementMultiplier = 50f;

    void Update()
    {
        // placeholder until input sources get defined
        // Move up
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) {
            controller.moveZAxis(moveSpeed * movementMultiplier);
        }

        // Move down
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) {
            controller.moveZAxis(moveSpeed * -1f * movementMultiplier);
        }
        else{
            controller.moveZAxis(0f);
        }

        // placeholder until input sources get defined
        // Move left
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
            controller.moveXAxis(moveSpeed * -1f *  movementMultiplier);
        }

        // Move right
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) {
            controller.moveXAxis(moveSpeed * movementMultiplier);
        }
        else{
            controller.moveXAxis(0f);
        }
    }
    
    private void Reset()
    {
        this.controller = GetComponent<PlayerController>();
    }
}
