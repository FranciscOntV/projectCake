using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(StatusManager))]
public class PlayerController : MonoBehaviour
{
    public PlayerIndicators indicators = new PlayerIndicators();
    public StatusManager stats;
    public Rigidbody rb;
    private Vector3 forward;
    private Vector3 interactPosition;
    private const float interactRadius = 0.8f;
    private GrabableItem grabbedItem;

    /// For debugging purposes only.
    public void OnDrawGizmos()
    {
        // Draw 'Joystick' position (white sphere)
        Gizmos.DrawWireSphere(this.forward, 0.5f);

        // Draw where the 'front' is being checked
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.interactPosition + Vector3.up, new Vector3(.4f, 1.8f, .4f));

        // Draw 'Interactable Radius' position (red sphere)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.interactPosition, interactRadius);
    }

    void Awake()
    {
        indicators.isGrounded = true;
        this.stats.initialize();
    }
    private void Update()
    {
        updateIndicators();
        checkLanding();
        // Update interactable radius position
        this.interactPosition = this.transform.GetChild(0).position;
    }

    /// <summary>
    /// Update the boolean flags in the indicators
    /// </summary>
    private void updateIndicators()
    {
        if (!indicators.isGrounded && isGrounded()) {
            indicators.landedLastFrame = true;
            indicators.isJumping = false;
        }
        else {
            indicators.landedLastFrame = false;
        }
        indicators.isGrounded = isGrounded();
    }

    /// <summary>
    /// Update the boolean flags in the indicators
    /// </summary>
    private void checkLanding()
    {
        if (indicators.landedLastFrame) {
            // TODO: add functionality
            Debug.Log("Landed");
        }
    }

    /// <summary>
    /// Triggers an interaction in the interaction radius.
    /// </summary>
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

    /// <summary>
    /// Move the character towards joystick position.
    /// </summary>
    /// <param name="speed">Speed and direction of the movement.</param>
    /// <param name="running">Is Running?.</param>
    public void moveCharacter(Vector3 speed, bool running)
    {
        float multiplier = stats.SPD * (running ? stats.runMultiplier : 1f);
        Vector3 finalSpeed = new Vector3(speed.x * multiplier, rb.velocity.y, speed.z * multiplier);
        this.rb.velocity = finalSpeed;
    }

    /// <summary>
    /// Makes the player jump
    /// </summary>
    public void jump()
    {
        indicators.isJumping = true;
        this.rb.velocity = new Vector3(rb.velocity.x, stats.JMP, rb.velocity.z);
    }

    /// <summary>
    /// Updates the character facing direction.
    /// </summary>
    public void updateDirection(Vector3 direction)
    {
        this.forward = (this.transform.position + direction);
        this.transform.LookAt(this.forward, Vector3.up);
    }

    /// <summary>
    /// Determines the direction and intensity to throw an item.
    /// </summary>
    /// <returns>
    /// Vector3 throw direction.
    /// </returns>
    public Vector3 getThrowVector()
    {
        return new Vector3(transform.forward.x * Common.throwForce, 0f, transform.forward.z * Common.throwForce);
    }

    /// <summary>
    /// Determines if the player is holding an item.
    /// </summary>
    /// <returns>
    /// bool true if holding something, otherwise else.
    /// </returns>
    public bool hasGrabbedItem()
    {
        return (this.grabbedItem != null);
    }

    /// <summary>
    /// Determines if the player can make a jump.
    /// </summary>
    public bool canJump()
    {
        // TODO: add more validations in the future.
        return isGrounded();
    }

    /// <summary>
    /// Checks if the player can walk forward
    /// </summary>
    public bool canWalkForward()
    {
        // TODO: Reduce overlap box size and/or position if slopes are planned
        Collider[] inFront = Physics.OverlapBox(this.interactPosition + Vector3.up, new Vector3(.2f, 0.9f, .2f));
        foreach (Collider front in inFront)
        {
            if (front.transform.tag == Common.terrainTag)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Determines if the player is currently grounded.
    /// </summary>
    /// <returns>
    /// bool true if on ground, otherwise else.
    /// </returns>
    public bool isGrounded()
    {
        // TODO: cast more rays.
        RaycastHit[] hits = Physics.RaycastAll(this.transform.position + new Vector3(0f, -.9f, 0f), Vector3.down, 0.2f);
        if (hits.Length > 0)
        {
            return true;
        }
        return false;
    }

    // triggered when the component is reset
    private void Reset()
    {
        this.stats = GetComponent<StatusManager>();
        this.rb = GetComponent<Rigidbody>();
    }
}
