using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;

public static class EventManager
{
    //public delegate void GameEvent(object e);
    //public delegate void GameEvent(object sender, EventArgs e);

    //private static Dictionary<string, UnityEvent> _eventDict = new Dictionary<string, UnityEvent>();
    private static Dictionary<string, UnityEvent> _eventDict = new Dictionary<string, UnityEvent>();

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (_eventDict.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            _eventDict.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (_eventDict.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (_eventDict.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();            
        }
    }

}