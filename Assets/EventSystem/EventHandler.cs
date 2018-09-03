// Copyright (C) 2016 Filip Cyrus Bober

using UnityEngine;

namespace FCB.EventSystem
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
