using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEvents;

    public class SceneTestBehaviour : MonoBehaviour
    {
        public GameEvent SceneCompletedEvent;
        public GameObject theCake;
        public Camera gameplayCamera;
        public GameObject[] path;
        public float speed = 1.0F;

        public float timeScale = 1.0F;

        private Transform currentTarget = null;
        public float targetGizmoRadius = 5.0f;

        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = timeScale;
            StartCoroutine(MoveCakeThruPath(this.path));
        }

        void OnDrawGizmos() {
            if (currentTarget != null) {
                Gizmos.color = Color.white;
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
                distCovered = (Time.time - startTime) * speed;

                // Fraction of journey completed = current distance divided by total distance.
                float fracJourney = distCovered / journeyLength;

                // Set our position as a fraction of the distance between the markers.
                theCake.transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);

                absRemainingDistance = journeyLength - distCovered;
                yield return null;
            }
            yield return null;
        }

        private IEnumerator MoveCakeThruPath(GameObject[] thePath)
        {
            foreach(GameObject obj in thePath) {
                currentTarget = obj.transform;
                yield return MoveCakeToPoint(
                    theCake.transform.position,
                    new Vector3(
                        obj.transform.position.x,
                        theCake.transform.position.y,
                        obj.transform.position.z
                    )
                );
            }
            SceneCompletedEvent.Raise();
        }
    }

