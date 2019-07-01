using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(menuName = "Game Events/GameEvent", order = 1)]
    public class GameEvent : ScriptableObject
    {
            protected List<GameEventListener> listeners = new List<GameEventListener>();

            public void Raise()
            {
                for (int idx = 0; idx < listeners.Count; idx++)
                {
                    listeners[0].OnEventRaised();
                }
            }

            public void RegisterListener(GameEventListener listener)
            {
                if (!listeners.Contains(listener))
                {
                    listeners.Add(listener);
                }
            }

            public void UnregisterListener(GameEventListener listener)
            {
                listeners.Remove(listener);
            }
    }
}
