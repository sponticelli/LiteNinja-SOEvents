using System;
using UnityEngine;
using UnityEngine.Events;

namespace LiteNinja.SOEvents
{
    [Serializable]
    public abstract class SOEventListener<T> : MonoBehaviour
    {
        protected abstract SOEvent<T> Event { get; }
        protected abstract UnityEvent<T> Action { get; }
        
        
        private void OnEnable()
        {
            Event?.RegisterListener(this);
        }
        
        private void OnDisable()
        {
            Event?.UnregisterListener(this);
        }
        
        public void Raise(T data)
        {
            Action?.Invoke(data);
        }
        
        /// <summary>
        /// if event has parameter, raise it again with the same data
        /// </summary>
        public void RaiseAgain()
        {
            //if event has parameter, raise it again
            if (Event.HasLastParameter)
                Raise(Event.LastParameter);
        }
    }
}