using UnityEngine;
using System.Collections;
using System;

public class EnemyPhysicsEvents : MonoBehaviour
{
    public event EventHandler<EnemyPropertiesEventArgs> DestinationReached;

    private EnemyProperties _enemyPropreties;

    public void Initialize(EnemyProperties enemyProperties)
    {
        _enemyPropreties = enemyProperties;
    }

    public void OnDestinationReached(EnemyPropertiesEventArgs e)
    {
        if (DestinationReached != null)
        {
            DestinationReached(this, e);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTags.DESTINATION))
        {
            OnDestinationReached(new EnemyPropertiesEventArgs(gameObject, _enemyPropreties));
        }
    }
}
