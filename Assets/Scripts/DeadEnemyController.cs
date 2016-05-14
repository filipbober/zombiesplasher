using UnityEngine;
using System.Collections;

public class DeadEnemyController : MonoBehaviour
{
    private IEnemyMover _mover;
    private EnemyData _enemyData;
    private IEnemyCollisionResponse _collisionResponse;

    public void Initialize(Transform destination, EnemyData enemyData)
    {
        _enemyData = enemyData;

        _collisionResponse = GetComponent<IEnemyCollisionResponse>();
        _collisionResponse.Initialize(_enemyData);

        _mover = GetComponent<IEnemyMover>();
        _mover.Initialize(null, destination, enemyData.Speed);
    }

    void OnEnable()
    {
        _collisionResponse.EnemyCollided += EnemyCollided;
    }

    void OnDisable()
    {
        _collisionResponse.EnemyCollided -= EnemyCollided;
    }

    void EnemyCollided(object sender, EnemyCollisionEventArgs e)
    {
        e.GameObj.SetActive(false);
    }
}
