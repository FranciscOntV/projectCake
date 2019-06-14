using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableItem : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider coll;

    void Awake()
    {
        coll = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if (this.isGrabbed())
        {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, Common.grabbedPosition, Common.timedValue(Common.grabSpeed));
        }
    }
    public void grab(Transform interactor)
    {
        this.coll.enabled = false;
        this.transform.SetParent(interactor);
        this.rb.useGravity = false;
    }

    public void drop(Vector3 direction)
    {
        this.coll.enabled = true;
        this.transform.eulerAngles = this.transform.parent.eulerAngles;
        this.transform.parent = null;
        this.rb.useGravity = true;
        this.rb.velocity= direction;
    }

    public bool isGrabbed()
    {
        return (this.transform.parent != null);
    }

    public bool isGrabable()
    {
        return (this.transform.parent == null);
    }
}
