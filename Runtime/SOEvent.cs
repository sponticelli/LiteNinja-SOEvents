using System;
using System.Collections.Generic;
using LiteNinja.Utils;
using UnityEngine;

namespace LiteNinja.SOEvents
{
    public class SOEvent<T> : DescribedSO, ISerializationCallbackReceiver
    {
        [SerializeField] private bool _raiseOnAdd;

        [NonSerialized] private readonly List<SOEventListener<T>> _eventListeners = new();
        [NonSerialized] private readonly List<Action<T>> _listeners = new();

        public bool HasLastParameter { get; private set; }
        public T LastParameter { get; private set; }


        public void Raise(T parameter)
        {
            
            foreach (var listener in _listeners)
            {
                listener.Invoke(parameter);
            }
            foreach (var listener in _eventListeners)
            {
                listener.Raise(parameter);
            }
            
            if (!_raiseOnAdd) return;
            HasLastParameter = true;
            LastParameter = parameter;
        }
        
        #region Register/unregister listeners
        public void RegisterListener(SOEventListener<T> listener)
        {
            if (listener == null) return;
            _eventListeners.Add(listener);
            if (_raiseOnAdd && HasLastParameter) listener.Raise(LastParameter);
        }
        
        public void RegisterListener(Action<T> listener)
        {
            if (listener == null) return;
            _listeners.Add(listener);
            if (_raiseOnAdd && HasLastParameter) listener.Invoke(LastParameter);
        }

        public void UnregisterListener(SOEventListener<T> listener)
        {
            if (listener == null) return;
            _eventListeners.Remove(listener);
        }
        
        public void UnregisterListener(Action<T> listener)
        {
            if (listener == null) return;
            _listeners.Remove(listener);
        }
        #endregion


        #region Serialization

        public void OnBeforeSerialize()
        {
            throw new System.NotImplementedException();
        }

        public void OnAfterDeserialize()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}