// Copyright (C) 2016 Filip Cyrus Bober

using System.Collections.Generic;

namespace FCB.EventSystem
{
    public class SingleEvent<T> where T : GameEvent
    {
        public int Id;

        private System.Type _eventType;

        // TODO: int id can be IObjectId interface get { return id}
        public SingleEvent(int id)
        {
            Id = id;
            _eventType = typeof(T);
        }

        protected bool Equals(SingleEvent<T> other)
        {
            return Id == other.Id && _eventType == other._eventType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SingleEvent<T>)obj);
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
        private Dictionary<int, EventDelegate> delegates = new Dictionary<int, EventDelegate>();
        //private Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();

        //public void AddListener<T>(SingleEvent id, EventDelegate<T> eventListener) where T : GameEvent
        //public void AddListener<T>(SingleEvent id, EventDelegate<T> eventListener) where T : GameEvent
        public void AddListener<T>(int objId, EventDelegate<T> eventListener) where T : GameEvent
        {
            //if (delegateLookup.ContainsKey(eventListener))            
            //    return;

            int id = new SingleEvent<T>(objId).GetHashCode();

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

        public void RemoveListener<T>(int objId, EventDelegate<T> eventListener) where T : GameEvent
        {
            int id = new SingleEvent<T>(objId).GetHashCode();

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

        //public void Raise<T>(int objId, GameEvent e) where T : GameEvent
        public void Raise<T>(int objId, T e) where T : GameEvent
        {
            System.Type type = e.GetType();
            int key = new SingleEvent<T>(objId).GetHashCode();

            EventDelegate eventInvoker;
            if (delegates.TryGetValue(key, out eventInvoker))
            {
                eventInvoker.Invoke(e);
            }



        }

    }
}
