using UnityEngine;

public class CorpseSpawner : MonoBehaviour
{
    public ObjectPooler DefaultEnemyCorpsePool;
    [SerializeField]
    private Enums.EnemyType _enemyType;

    private ObjectPooler _corpsePool;

    void Start()
    {
        //_corpsePool = PoolManager
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
        GameObject go = DefaultEnemyCorpsePool.GetPooledObject();
        go.transform.position = e.EnemyGameObj.transform.position;
        go.transform.rotation = e.EnemyGameObj.transform.rotation;

        go.SetActive(true);
    }

}
