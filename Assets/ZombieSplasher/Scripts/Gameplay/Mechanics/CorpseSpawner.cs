using UnityEngine;

public class CorpseSpawner : MonoBehaviour
{
    public ObjectPooler DefaultEnemyCorpsePool;
    [SerializeField]
    private Enums.ActorType _enemyType;

    private ObjectPooler _corpsePool;

    void Start()
    {
        //_corpsePool = PoolManager
        _corpsePool = PoolManager.Instance.GetActorPool(_enemyType);
    }

    void OnEnable()
    {
        EnemyController.EnemyDown += SpawnEnemyCorpse;
    }

    void OnDisable()
    {
        EnemyController.EnemyDown -= SpawnEnemyCorpse;
    }

    protected void SpawnEnemyCorpse(object sneder, EnemyPropertiesEventArgs e)
    {
        //GameObject go = DefaultEnemyCorpsePool.GetPooledObject();
        GameObject go = _corpsePool.GetPooledObject();
        go.transform.position = e.EnemyGameObj.transform.position;
        go.transform.rotation = e.EnemyGameObj.transform.rotation;

        go.SetActive(true);
    }

}
