using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 0f, 0f);
    public Transform follow;
    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, follow.transform.position + offset, Common.timedValue(followSpeed));
    }
}
