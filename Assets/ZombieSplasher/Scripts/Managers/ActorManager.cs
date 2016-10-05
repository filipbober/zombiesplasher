using UnityEngine;
using System.Collections.Generic;
using System;

public class ActorManager : MonoBehaviour
{
    [SerializeField]
    GameObject _objectPoolPrefab;

    [SerializeField]
    private ActorLookup _actorLookup;

    public static ActorManager Instance
    {
        get { return _instance; }
    }

    private static ActorManager _instance;

    private Dictionary<Enums.ActorType, ObjectPooler> _poolDict = new Dictionary<Enums.ActorType, ObjectPooler>();

    public ObjectPooler GetActorPool(Enums.ActorType type)
    {
        ObjectPooler pool;
        if (_poolDict.TryGetValue(type, out pool))
            return pool;
        else
            return null;
    }

    public GameObject GetActorObj(Enums.ActorType type)
    {
        ObjectPooler pool;
        if (_poolDict.TryGetValue(type, out pool))
        {
            return pool.GetPooledObject();
        }

        return null;
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
    }

    void Start()
    {
        foreach (Enums.ActorType enemyType in Enum.GetValues(typeof(Enums.ActorType)))
        {
            ActorProperties properties;
            if (!_actorLookup.ActorsProperties.TryGetValue(enemyType, out properties))
                continue;

            GameObject obj = Instantiate(_objectPoolPrefab);
            obj.transform.SetParent(transform);
            ObjectPooler pool = obj.GetComponent<ObjectPooler>();

            GameObject newPoolObject;
            if (_actorLookup.ActorsGameObjects.TryGetValue(enemyType, out newPoolObject))
            {
                pool.PooledObject = newPoolObject;
                pool.PooledAmount = properties.PoolAmout;


                if (_actorLookup.ActorsProperties.TryGetValue(enemyType, out properties))
                {
                    _poolDict.Add(enemyType, obj.GetComponent<ObjectPooler>());
                    Debug.Log("Creating new pool");
                }
            }

        }
    }

}
