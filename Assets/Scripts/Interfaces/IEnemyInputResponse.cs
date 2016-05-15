using UnityEngine;
using System.Collections;

public interface IEnemyInputResponse
{
    event System.EventHandler<EnemyPropertiesEventArgs> EnemyClicked;

    void Initialize(EnemyProperties enemyProperties);
    void OnEnemyClicked(EnemyPropertiesEventArgs e);
}
