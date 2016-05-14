using UnityEngine;
using System.Collections;

public interface IEnemyCollisionResponse
{
    event System.EventHandler<EnemyCollisionEventArgs> EnemyCollided;

    void Initialize(EnemyData enemyData);
    void OnEnemyTrigger(EnemyCollisionEventArgs e);
}
