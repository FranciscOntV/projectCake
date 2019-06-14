using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public StatusManager stats;
    public Rigidbody rb;
    private Vector3 forward;
    private Vector3 interactPosition;
    private const float interactRadius = 0.8f;
    private GrabableItem grabbedItem;

    public void OnDrawGizmos()
    {
        // Draw 'Joystick' position (white sphere)
        Gizmos.DrawWireSphere(this.forward, 0.5f);

        // Draw 'Interactable Radius' position (red sphere)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.interactPosition, interactRadius);
    }

    void Awake()
    {
        this.stats.initialize();
    }
    private void Update()
    {
        // Update interactable radius position
        this.interactPosition = this.transform.GetChild(0).position;
    }
    public void interact()
    {
        // check for every object in interactable radius
        Collider[] interactable = Physics.OverlapSphere(this.interactPosition, interactRadius);
        foreach (Collider t in interactable)
        {
            // usable item  ??
            if (t.transform.tag == Common.usableTag)
            {
                // TODO == check for usable items
            }
            // Grabable Items
            if (t.transform.tag == Common.grabableTag)
            {
                GrabableItem i = t.GetComponent<GrabableItem>();
                // found an item! 
                if (i != null && i.isGrabable())
                {
                    // if already has a item, swap
                    if (this.hasGrabbedItem())
                    {
                        this.grabbedItem.drop(this.interactPosition + Common.dropPosition);
                        i.grab(this.transform);
                        this.grabbedItem = i;
                        return;
                    }
                    //if not, grab
                    else
                    {
                        i.grab(this.transform);
                        this.grabbedItem = i;
                        return;
                    }

                }
            }
        }
        // if no item is foun, drop the items
        if (this.hasGrabbedItem())
        {
            this.grabbedItem.drop(this.getThrowVector());
            this.grabbedItem = null;
        }
    }

    // Move the character towards joystick position
    public void moveCharacter(Vector3 speed)
    {
        Vector3 finalSpeed = new Vector3(speed.x * stats.getWalkSpeed(), 0f, speed.z * stats.getWalkSpeed());
        this.rb.velocity = finalSpeed;
    }

    // Update movement direction
    public void updateDirection(Vector3 direction)
    {
        this.forward = (this.transform.position + direction);
        this.transform.LookAt(this.forward, Vector3.up);
    }

    public Vector3 getThrowVector() {
        float x = (this.interactPosition.x - this.transform.position.x) * Common.throwForce;
        float z = (this.interactPosition.z - this.transform.position.z) * Common.throwForce;
        return new Vector3(x,0f,z);
    }

    // Check if playes has an item
    public bool hasGrabbedItem()
    {
        return (this.grabbedItem != null);
    }

    // triggered when the component is reset
    private void Reset()
    {
        this.stats = GetComponent<StatusManager>();
        this.rb = GetComponent<Rigidbody>();
    }
}
