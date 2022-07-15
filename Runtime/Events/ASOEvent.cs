using System;
using System.Collections.Generic;
using LiteNinja.Utils;
using UnityEngine;

namespace LiteNinja.SOEvents
{
  

    public abstract class ASOEvent<T> : DescribedSO, ISerializationCallbackReceiver
    {
        [SerializeField] private bool _raiseOnAdd;

        [NonSerialized] private readonly List<ASOEventListener<T>> _eventListeners = new();
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
                listener.OnEventRaised(parameter);
            }
            
            if (!_raiseOnAdd) return;
            HasLastParameter = true;
            LastParameter = parameter;
        }
        
        #region Register/unregister listeners
        public void Register(ASOEventListener<T> listener)
        {
            if (listener == null) return;
            _eventListeners.Add(listener);
            if (_raiseOnAdd && HasLastParameter) listener.OnEventRaised(LastParameter);
        }
        
        public void Register(Action<T> listener)
        {
            if (listener == null) return;
            _listeners.Add(listener);
            if (_raiseOnAdd && HasLastParameter) listener.Invoke(LastParameter);
        }

        public void Unregister(ASOEventListener<T> listener)
        {
            if (listener == null) return;
            _eventListeners.Remove(listener);
        }
        
        public void Unregister(Action<T> listener)
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