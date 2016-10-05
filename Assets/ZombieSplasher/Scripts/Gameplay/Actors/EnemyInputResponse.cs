using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class EnemyInputResponse : MonoBehaviour, IPointerClickHandler, IActorInputResponse
{
    public event EventHandler<ActorPropertiesEventArgs> EnemyClicked;

    private ActorProperties _properties;

    public void Initialize(ActorProperties actorProperties)
    {
        _properties = actorProperties;
    }

    public void OnActorClicked(ActorPropertiesEventArgs e)
    {
        if (EnemyClicked != null)
        {
            EnemyClicked(this, e);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnActorClicked(new ActorPropertiesEventArgs(gameObject, _properties));
    }
}
