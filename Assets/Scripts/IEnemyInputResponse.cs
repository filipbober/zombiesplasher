using UnityEngine;
using System.Collections;

public interface IEnemyInputResponse
{
    event System.EventHandler<EnemyClickedEventArgs> EnemyClicked;

    void Initialize(EnemyData enemyData);
    void OnEnemyClicked(EnemyClickedEventArgs e);
}
