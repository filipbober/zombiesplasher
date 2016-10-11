using UnityEngine;

public class ActorPropertiesEvent : FCB.EventSystem.GameEvent
{
    public GameObject Sender { get; set; }
    public ActorProperties ActorProperties;

    public ActorPropertiesEvent(GameObject sender, ActorProperties actorProperties)
    {
        Sender = sender;
        ActorProperties = actorProperties;
    }
}

