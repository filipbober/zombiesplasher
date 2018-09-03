// Copyright (C) 2016 Filip Cyrus Bober

using UnityEngine;

namespace ZombieSplasher
{
    public class ActorClickedSingleEvent : ActorPropertiesEvent, FCB.EventSystem.ISingleEvent
    {
        public ActorClickedSingleEvent(GameObject sender, ActorProperties actorProperties) : base(sender, actorProperties)
        {
        }
    }
}