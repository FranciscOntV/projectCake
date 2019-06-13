using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;


    public void moveXAxis(float value)
    {
        Vector3 movement = new Vector3(Common.timedValue(value), controller.velocity.y, controller.velocity.z);
        this.moveCharacter(movement);
    }

    public void moveZAxis(float value)
    {
        Vector3 movement = new Vector3(controller.velocity.x, controller.velocity.y, Common.timedValue(value));
        this.moveCharacter(movement);
    }
    private void moveCharacter(Vector3 speed)
    {
        controller.SimpleMove(speed);
    }

    private void Reset()
    {
        this.controller = GetComponent<CharacterController>();
    }
}
