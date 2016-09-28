using UnityEngine;
using System.Collections.Generic;
using System;

public class PoolManager : MonoBehaviour
{
    [SerializeField]
    GameObject _objectPoolPrefab;

    [SerializeField]
    private ActorLookup _actorLookup;

    public static PoolManager Instance
    {
        get { return _instance; }
    }

    private static PoolManager _instance;

    private Dictionary<Enums.ActorType, ObjectPooler> _poolDict = new Dictionary<Enums.ActorType, ObjectPooler>();

    public ObjectPooler GetActorPool(Enums.ActorType type)
    {
        // TODO: check null
        ObjectPooler pool;
        if (_poolDict.TryGetValue(type, out pool))
            return pool;
        else
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
        Debug.Log("Start");
        //var actorTypes = Enum.GetValues(typeof(Enums.ActorType));
        Dictionary<Enums.ActorType, ActorProperties> actorProperties = new Dictionary<Enums.ActorType, ActorProperties>();
        Debug.Log("Lookup = " + _actorLookup.Actors.Count);
        foreach (var actor in _actorLookup.Actors)
        {
            ActorProperties properties = actor.Value.GetComponent<ActorProperties>();
            actorProperties.Add(properties.ActorType, properties);
        }
        Debug.Log("actorProperties dict = " + actorProperties.Count);

        foreach (Enums.ActorType enemyType in Enum.GetValues(typeof(Enums.ActorType)))
        {
            Debug.Log("Invoking foreach for: " + enemyType);
            ActorProperties properties;
            if (!actorProperties.TryGetValue(enemyType, out properties))
                continue;

            GameObject obj = Instantiate(_objectPoolPrefab);
            obj.transform.SetParent(transform);
            ObjectPooler pool = obj.GetComponent<ObjectPooler>();

            GameObject newPoolObject;
            if (_actorLookup.Actors.TryGetValue(enemyType, out newPoolObject))
            {
                pool.PooledObject = newPoolObject;


                if (actorProperties.TryGetValue(enemyType, out properties))
                {
                    _poolDict.Add(enemyType, obj.GetComponent<ObjectPooler>());
                    Debug.Log("Creating new pool");
                }
            }


        }
    }

}
