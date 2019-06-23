using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableItem : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider coll;
    private CapsuleCollider ignoreTarget;
    private float ignoreTimer = 0f;
    private bool ignoreCollision = false;

    void Awake()
    {
        coll = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if (isGrabbed())
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Common.grabbedPosition, Common.timedValue(Common.grabSpeed));
        }
        else
        {
            updateIgnoreCollisionTimer();
        }
    }

    /// <summary>
    /// Updates the collision ignore timer to avoid falling over another character.
    /// </summary>
    private void updateIgnoreCollisionTimer()
    {
        if (ignoreTimer > 0f)
        {
            ignoreCollision = true;
            ignoreTimer -= Common.timedValue();
            Physics.IgnoreCollision(coll, ignoreTarget, ignoreCollision);
        }
        else
        {
            if (ignoreCollision)
            {
                ignoreCollision = false;
                Physics.IgnoreCollision(coll, ignoreTarget, ignoreCollision);
            }
        }
    }

    /// <summary>
    /// Actions to execute when grabbed.
    /// </summary>
    public void grab(Transform interactor)
    {
        // TODO: change type if other collider is used for characters.
        ignoreTarget = interactor.GetComponent<CapsuleCollider>();
        coll.enabled = false;
        transform.SetParent(interactor);
        rb.useGravity = false;
    }

    /// <summary>
    /// Actions to execute when dropped.
    /// </summary>
    public void drop(Vector3 direction)
    {
        ignoreTimer = 0.5f;
        coll.enabled = true;
        transform.eulerAngles = transform.parent.eulerAngles;
        transform.parent = null;
        rb.useGravity = true;
        rb.velocity = direction;
    }

    /// <summary>
    /// Determines if the object is already grabbed.
    /// </summary>
    /// <returns>
    /// bool true if already grabbed, otherwise else.
    /// </returns>
    public bool isGrabbed()
    {
        return (transform.parent != null);
    }

    /// <summary>
    /// Determines if the object can be grabbed.
    /// </summary>
    /// <returns>
    /// bool true if can be grabbed, otherwise else.
    /// </returns>
    public bool isGrabable()
    {
        return (transform.parent == null);
    }
}
