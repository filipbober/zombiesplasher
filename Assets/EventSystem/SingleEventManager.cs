// Copyright (C) 2016 Filip Cyrus Bober

using System.Collections.Generic;

namespace FCB.EventSystem
{
    public class SingleEvent
    {
        public int Id;
        public GameEvent Event;

        public SingleEvent(int id, GameEvent @event)
        {
            Id = id;
            Event = @event;
        }
    }

    public class SingleEventManager
    {
        public static SingleEventManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SingleEventManager();
                }

                return _instance;
            }
        }

        public int ListenerCount { get { return delegateLookup.Count; } }

        private static SingleEventManager _instance = null;

        public delegate void EventDelegate<T>(T e) where T : GameEvent;
        private delegate void EventDelegate(GameEvent e);

        private Dictionary<int, EventDelegate> delegates = new Dictionary<int, EventDelegate>();
        private Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();

        public void AddListener<T>(int id, EventDelegate<T> eventListener) where T : GameEvent
        {
            if (delegateLookup.ContainsKey(eventListener))            
                return;
            
            if (delegates.ContainsKey(id))
                return;

            EventDelegate internalDelegate = (e) => eventListener((T)e);

            delegateLookup[eventListener] = internalDelegate;

            System.Type eventType = typeof(T);
            EventDelegate eventInvoker;
            if (delegates.TryGetValue(id, out eventInvoker))
            {
                eventInvoker += internalDelegate;
                delegates[id] = eventInvoker;
            }
            else
            {
                delegates[id] = internalDelegate;
            }
        }

        public void RemoveListener<T>(int id, EventDelegate<T> eventListener) where T : GameEvent
        {
            EventDelegate internalDelegate;
            if (delegateLookup.TryGetValue(eventListener, out internalDelegate))
            {
                System.Type eventType = typeof(T);
                EventDelegate eventInvoker;
                if (delegates.TryGetValue(id, out eventInvoker))
                {
                    eventInvoker -= internalDelegate;
                    if (eventInvoker == null)
                    {
                        delegates.Remove(id);
                    }
                    else
                    {
                        delegates[id] = eventInvoker;
                    }
                }

                delegateLookup.Remove(eventListener);
            }
        }

        public void Raise(SingleEvent e)
        {
            int key = e.Id;
            EventDelegate eventInvoker;
            if (delegates.TryGetValue(key, out eventInvoker))
            {
                eventInvoker.Invoke(e.Event);
            }



        }

    }
}
