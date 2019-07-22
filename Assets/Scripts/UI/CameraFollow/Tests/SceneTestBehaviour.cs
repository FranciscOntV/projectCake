using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEvents;
    public class SceneTestBehaviour : MonoBehaviour
    {
        public GameEvent SceneCompletedEvent;
        public GameEventListener MainSecuenceCompletedListener;
        public Camera gameplayCamera;

        public Collider[] players;
        public Collider cake;

        public float timeScale = 1.0F;
        void Start()
        {
            //Time.timeScale = timeScale;
        }
    }

