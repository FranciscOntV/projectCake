using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEvents;

public class MoveStaticThruPath : MonoBehaviour
{
    public float Speed = 1;
    public GameObject[] Path;
    public GameEventS1 OnPathFinished;


    private Transform currentTarget = null;
    public float targetGizmoRadius = 1.0f;
    public Color targetGizmoColor = Color.red;


    void Start()
    {
        StartCoroutine(MoveCakeThruPath());
    }

    void OnDrawGizmos() {
        if (currentTarget != null) {
            Gizmos.color = targetGizmoColor;
            Gizmos.DrawWireSphere(currentTarget.position, targetGizmoRadius);
        }
    }

    private IEnumerator MoveCakeToPoint(Vector3 startMarker, Vector3 endMarker)
    {
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(startMarker, endMarker);
        float distCovered = 0;

        float absRemainingDistance = journeyLength - distCovered;
        while (absRemainingDistance > 0.01F)
        {
            // Distance moved = time * speed.
            distCovered = (Time.time - startTime) * Speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            this.transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);

            absRemainingDistance = journeyLength - distCovered;
            yield return null;
        }
        yield return null;
    }

    private IEnumerator MoveCakeThruPath()
    {
        foreach(GameObject obj in Path) {
            currentTarget = obj.transform;
            yield return MoveCakeToPoint(
                this.transform.position,
                new Vector3(
                    obj.transform.position.x,
                    this.transform.position.y,
                    obj.transform.position.z
                )
            );
        }
        if(OnPathFinished != null) {
            OnPathFinished.Raise(this.name);
        }
    }
}
