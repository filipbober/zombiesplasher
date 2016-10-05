// Copyright (C) 2016 Filip Cyrus Bober

using System.Collections.Generic;

namespace EventSystem
{
    /// <summary>
    /// This class is a minor variation on<http://www.willrmiller.com/?p=87>
    /// </summary>
    public class EventManager
    {
        public static EventManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventManager();
                }

                return _instance;
            }
        }

        public int ListenerCount { get { return delegateLookup.Count; } }

        private static EventManager _instance = null;

        public delegate void EventDelegate<T>(T e) where T : GameEvent;
        private delegate void EventDelegate(GameEvent e);

        private Dictionary<System.Type, EventDelegate> delegates = new Dictionary<System.Type, EventDelegate>();
        private Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();

        public void AddListener<T>(EventDelegate<T> eventListener) where T : GameEvent
        {
            if (delegateLookup.ContainsKey(eventListener))
            {
                return;
            }

            EventDelegate internalDelegate = (e) => eventListener((T)e);

            delegateLookup[eventListener] = internalDelegate;

            System.Type eventType = typeof(T);
            EventDelegate eventInvoker;
            if (delegates.TryGetValue(eventType, out eventInvoker))
            {
                eventInvoker += internalDelegate;
                delegates[eventType] = eventInvoker;
            }
            else
            {
                delegates[eventType] = internalDelegate;
            }
        }

        public void RemoveListener<T>(EventDelegate<T> eventListener) where T : GameEvent
        {
            EventDelegate internalDelegate;
            if (delegateLookup.TryGetValue(eventListener, out internalDelegate))
            {
                System.Type eventType = typeof(T);
                EventDelegate eventInvoker;
                if (delegates.TryGetValue(eventType, out eventInvoker))
                {
                    eventInvoker -= internalDelegate;
                    if (eventInvoker == null)
                    {
                        delegates.Remove(eventType);
                    }
                    else
                    {
                        delegates[eventType] = eventInvoker;
                    }
                }

                delegateLookup.Remove(eventListener);
            }
        }

        public void Raise(GameEvent e)
        {
            EventDelegate eventInvoker;
            if (delegates.TryGetValue(e.GetType(), out eventInvoker))
            {
                eventInvoker.Invoke(e);
            }
        }

    }
}
