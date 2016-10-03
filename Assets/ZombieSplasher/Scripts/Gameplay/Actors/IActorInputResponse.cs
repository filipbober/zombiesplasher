using UnityEngine;
using System.Collections;

public interface IActorInputResponse
{
    event System.EventHandler<ActorPropertiesEventArgs> EnemyClicked;

    void Initialize(ActorProperties enemyProperties);
    void OnActorClicked(ActorPropertiesEventArgs e);
}
