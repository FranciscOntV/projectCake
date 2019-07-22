using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using GameEvents;

namespace Tests
{
    public class GameplayCameraTest
    {
        private bool isSceneCompleted = false;
        public void SceneCompleted() {
            isSceneCompleted = true;
        }

        [UnityTest]
        public IEnumerator TestSceneSequenceCompletes()
        {
            SceneManager.LoadScene("Assets/Scripts/UI/CameraFollow/Tests/CameraFollow_test.unity");
            yield return null;

            SceneTestBehaviour sceneBehaviour = (SceneTestBehaviour)Object.FindObjectOfType(typeof(SceneTestBehaviour));
            Assert.True(sceneBehaviour != null, "No SceneTestBehaviour found");

            // Set up for the sequence complete event
            GameEventListener eventListener =  sceneBehaviour.gameObject.AddComponent<GameEventListener>();
            eventListener.enabled = false;
            eventListener.Event = sceneBehaviour.SceneCompletedEvent;
            eventListener.Response = new UnityEvent();
            eventListener.Response.AddListener(SceneCompleted);
            eventListener.enabled = true;

            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();


            Camera mainCamera = sceneBehaviour.gameplayCamera;
            Assert.True(mainCamera != null, "Camera no set in test scene.");

            Collider theCakeCollider = sceneBehaviour.cake;
            while (!isSceneCompleted && stopWatch.Elapsed.Seconds < 5)
            {
                Plane[] cameraFrustum = GeometryUtility.CalculateFrustumPlanes(mainCamera);
                Assert.True(// Cake always visible
                    GeometryUtility.TestPlanesAABB(cameraFrustum, theCakeCollider.bounds),
                    $"The Cake ({theCakeCollider.gameObject.name}) is not visible by camera ({mainCamera.name})."
                );
                yield return null;
            }
            stopWatch.Stop();

            // Secuence must complete in less than 5 seconds
            Assert.True(stopWatch.Elapsed.Seconds < 5, "Test timeout > 5 seconds.");
        }
    }
}
