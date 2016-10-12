using UnityEngine;

namespace ZombieSplasher
{
    public class ActorPropertiesEvent : FCB.EventSystem.GameEvent
    {
        public readonly GameObject Sender;
        public readonly ActorProperties ActorProperties;

        public ActorPropertiesEvent(GameObject sender, ActorProperties actorProperties)
        {
            Sender = sender;
            ActorProperties = actorProperties;
        }
    }
}

