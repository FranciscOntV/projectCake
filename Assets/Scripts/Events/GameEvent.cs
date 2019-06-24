﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : MonoBehaviour
{
        private List<GameEventListener> listeners = new List<GameEventListener>();

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