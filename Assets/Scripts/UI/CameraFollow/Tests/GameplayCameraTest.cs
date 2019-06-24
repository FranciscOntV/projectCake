using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace Tests
{
    public class GameplayCameraTest
    {
        private bool isSceneCompleted = false;
        public void SceneCompleted() {
            isSceneCompleted = true;
        }


        // A Test behaves as an ordinary method
        [Test]
        public void GameplayCameraTestSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator GameplayCameraTestWithEnumeratorPasses()
        {
            SceneManager.LoadScene("Assets/Tests/PlayMode Tests/Camera/CameraFollow/CameraFollow_test.unity");
            yield return null;

            SceneTestBehaviour sceneBehaviour = (SceneTestBehaviour)Object.FindObjectOfType(typeof(SceneTestBehaviour));
            Assert.True(sceneBehaviour != null);
            sceneBehaviour.timeScale = 15;

            GameEventListener eventListener = new GameEventListener();
            eventListener.Event = sceneBehaviour.SceneCompletedEvent;
            eventListener.Response = new UnityEvent();
            eventListener.Response.AddListener(SceneCompleted);


            float startTime = Time.time;
            float elapsedTime = 0;
            while ( (elapsedTime = Time.time - startTime) < 5 || isSceneCompleted)
            {
                Debug.Log("Elapsed time: " + elapsedTime);
                yield return null;
            }

            Assert.True(elapsedTime < 5);



            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            // yield return null;
        }
    }
}
