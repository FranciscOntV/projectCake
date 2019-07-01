using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Game Events/GameEventS1")]
    public class GameEventS1 : ScriptableObject
    {
        protected List<GameEventListenerS1> listeners = new List<GameEventListenerS1>();

        public void Raise(string s1)
        {
            for (int idx = 0; idx < listeners.Count; idx++)
            {
                listeners[0].OnEventRaised(s1);
            }
        }

        public void RegisterListener(GameEventListenerS1 listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }

        public void UnregisterListener(GameEventListenerS1 listener)
        {
            listeners.Remove(listener);
        }
    }
}
