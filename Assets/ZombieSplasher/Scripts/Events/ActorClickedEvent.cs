using UnityEngine;

namespace ZombieSplasher
{
    public class ActorClickedEvent : ActorPropertiesEvent
    {
        public ActorClickedEvent(GameObject sender, ActorProperties actorProperties) : base(sender, actorProperties)
        {
        }
    }
}