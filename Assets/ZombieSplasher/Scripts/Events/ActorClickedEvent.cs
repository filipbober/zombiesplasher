using UnityEngine;

public class ActorClickedEvent : ActorPropertiesEvent
{
    public ActorClickedEvent(GameObject sender, ActorProperties actorProperties) : base(sender, actorProperties)
    {
    }
}
