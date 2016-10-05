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
