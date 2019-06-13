using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Range(1f, 3f)]
    public PlayerController controller;
    private float moveX = 0f;
    private float moveZ = 0f;

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical"); 
        Vector3 movement = new Vector3(moveX, 0f, moveZ);
        if (moveZ != 0f || moveX != 0f) {
            controller.updateDirection(movement);
        }
        controller.moveCharacter(movement);
    }
    private void Reset()
    {
        this.controller = GetComponent<PlayerController>();
    }
}
