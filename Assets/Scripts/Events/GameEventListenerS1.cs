using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameEvents;

namespace GameEvents
{
    public class GameEventListenerS1 : MonoBehaviour
    {
        public GameEventS1 Event;
        public UnityEvent<string> Response;

        private void OnEnable()
        {
            if (Event != null)
            {
                Event.RegisterListener(this);
            }
        }

        private void OnDisable()
        {
            if (Event != null)
            {
                Event.UnregisterListener(this);
            }
        }

        public void OnEventRaised(string s1)
        {
            if (Response != null)
            {
                Response.Invoke(s1);
            }
        }
    }
}
