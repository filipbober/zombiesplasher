using UnityEngine;
using System.Collections;

public class ActorPropertiesEventArgs : System.EventArgs
{

    public GameObject EnemyGameObj { get; set; }
    //public EnemyProperties EnemyProperties;
    public ActorProperties ActorProperties;

    public ActorPropertiesEventArgs(GameObject enemyGameObj, ActorProperties actorProperties)
    {
        EnemyGameObj = enemyGameObj;
        ActorProperties = actorProperties;
    }
}

// TODO: zastanowic sie jak przesylac "object sender" i "GameObject" sender w eventach
public class ActorPropertiesEvent: EventSystem.GameEvent
{
    public object Sender { get; set; }
    public GameObject EnemyGameObj { get; set; }
    //public EnemyProperties EnemyProperties;
    public ActorProperties ActorProperties;

    public ActorPropertiesEvent(GameObject enemyGameObj, ActorProperties actorProperties, object sender)
    {
        Sender = sender;
        EnemyGameObj = enemyGameObj;
        ActorProperties = actorProperties;
    }
}
