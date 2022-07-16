using System;
using System.Collections.Generic;
using LiteNinja.Utils;
using UnityEngine;

namespace LiteNinja.SOEvents
{
    [CreateAssetMenu(menuName = "LiteNinja/Events/Game Event")]
    [Serializable]
    public class GameEvent : DescribedSO
    {
        [NonSerialized] private readonly List<GameEventListener> _eventListeners = new();
        [NonSerialized] private readonly List<Action> _listeners = new();

        public void Raise()
        {
            foreach (var listener in _listeners)
            {
                listener.Invoke();
            }
            
            foreach (var listener in _eventListeners)
            {
                listener.OnEventRaised();
            }
        }
        public void Register(GameEventListener listener)
        {
            _eventListeners.Add(listener);
        }
        public void Register(Action listener)
        {
            _listeners.Add(listener);
        }
        public void Unregister(GameEventListener listener)
        {
            
            _eventListeners.Remove(listener);
        }
        public void Unregister(Action listener)
        {
            if (!_listeners.Contains(listener))  return;
            _listeners.Remove(listener);
        }

       
    }
}