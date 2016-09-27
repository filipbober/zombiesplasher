using UnityEngine;
using System.Collections.Generic;
using System;

public class PoolManager : MonoBehaviour
{
    [SerializeField]
    GameObject _objectPoolPrefab;

    public static PoolManager Instance
    {
        get { return _instance; }
    }

    private static PoolManager _instance;

    private Dictionary<Enums.EnemyType, ObjectPooler> _poolDict = new Dictionary<Enums.EnemyType, ObjectPooler>();

    public ObjectPooler GetPool(Enums.EnemyType type)
    {
        // TODO: check null
        ObjectPooler pool;
        _poolDict.TryGetValue(type, out pool);
        Debug.Log("Pool = " + pool);
        return pool;
    }

    void Awake()
    {
        // Setup the singleton instance
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        foreach (Enums.EnemyType enemyType in Enum.GetValues(typeof(Enums.EnemyType)))
        {
            GameObject obj = (GameObject)Instantiate(_objectPoolPrefab);
            obj.transform.SetParent(transform);
            ObjectPooler pooler = obj.GetComponent<ObjectPooler>();

            _poolDict.Add(enemyType, obj.GetComponent<ObjectPooler>());            
        }

    }

}
