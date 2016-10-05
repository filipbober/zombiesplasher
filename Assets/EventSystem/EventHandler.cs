using UnityEngine;
using System.Collections;
using System;

namespace EventSystem
{
    public abstract class EventHandler : MonoBehaviour, IEventHandler
    {
        public abstract void SubscribeEvents();
        public abstract void UnsubscribeEvents();

        protected virtual void OnEnable()
        {
            SubscribeEvents();
        }

        protected virtual void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}
