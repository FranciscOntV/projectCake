using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterClass character;
    public Rigidbody rb;
    private Vector3 forward;

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.forward, 0.5f);
    }

    public void moveCharacter(Vector3 speed)
    {
        Vector3 finalSpeed = new Vector3(speed.x * character.moveSpeed, 0f, speed.z * character.moveSpeed);
        this.rb.velocity = finalSpeed;
    }

    public void updateDirection(Vector3 direction) {
        this.forward = (this.transform.position + direction);
        this.transform.LookAt(this.forward,Vector3.up);
    }

    private void Reset()
    {
        this.rb = GetComponent<Rigidbody>();
    }
}
