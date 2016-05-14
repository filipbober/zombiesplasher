using UnityEngine;
using System.Collections;
using System;

public class EnemyCollisionResponse : MonoBehaviour, IEnemyCollisionResponse
{
    public event EventHandler<EnemyCollisionEventArgs> EnemyCollided;

    private EnemyData _enemyData;

    public void Initialize(EnemyData enemyData)
    {
        _enemyData = enemyData;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTags.DESTINATION))
        {
            OnEnemyTrigger(new EnemyCollisionEventArgs(gameObject, _enemyData, other));
        }
    }

    public void OnEnemyTrigger(EnemyCollisionEventArgs e)
    {
        if (EnemyCollided != null)
        {
            EnemyCollided(this, e);
        }
    }
}
