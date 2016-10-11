// Copyright (C) 2016 Filip Cyrus Bober

using System.Collections.Generic;

namespace FCB.EventSystem
{
    public class SingleEvent
    {
        public int Id;
        public GameEvent Event;

        private System.Type _eventType;

        public SingleEvent(int id, GameEvent gameEvent)
        {
            Id = id;
            Event = gameEvent;
            _eventType = gameEvent.GetType();
        }

        protected bool Equals(SingleEvent other)
        {
            return Id == other.Id && _eventType == other._eventType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SingleEvent)obj);
        }

        public override int GetHashCode()
        {
            unchecked { return (Id * 397) ^ _eventType.GetHashCode(); }
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

        //public int ListenerCount { get { return delegateLookup.Count; } }

        private static SingleEventManager _instance = null;

        public delegate void EventDelegate<T>(T e) where T : GameEvent;
        private delegate void EventDelegate(GameEvent e);

        // TODO: Remove delegateLookup and replace int with SingleEvent hash
        //private Dictionary<int, EventDelegate> delegates = new Dictionary<int, EventDelegate>();
        private Dictionary<SingleEvent, EventDelegate> delegates = new Dictionary<SingleEvent, EventDelegate>();
        //private Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();

        //public void AddListener<T>(SingleEvent id, EventDelegate<T> eventListener) where T : GameEvent
        public void AddListener<T>(SingleEvent id, EventDelegate<T> eventListener) where T : GameEvent
        {
            //if (delegateLookup.ContainsKey(eventListener))            
            //    return;

            
            if (delegates.ContainsKey(id))
                return;

            EventDelegate internalDelegate = (e) => eventListener((T)e);

            //delegateLookup[eventListener] = internalDelegate;

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

        public void RemoveListener<T>(SingleEvent id, EventDelegate<T> eventListener) where T : GameEvent
        {
            EventDelegate internalDelegate;
            //if (delegateLookup.TryGetValue(eventListener, out internalDelegate))
            if (delegates.TryGetValue(id, out internalDelegate))
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

                //delegateLookup.Remove(eventListener);
            }
        }

        public void Raise(SingleEvent e)
        {
            int key = e.Id;
            EventDelegate eventInvoker;
            if (delegates.TryGetValue(e, out eventInvoker))
            {
                eventInvoker.Invoke(e.Event);
            }



        }

    }
}
