using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public static event System.EventHandler<EnemyPropertiesEventArgs> EnemySpawned;

    [SerializeField]
    private ObjectPooler _enemyPool;

    [SerializeField]
    float _spawnRate;

    //Transform[] _destinations;

    float _currentCooldown;

    // void Start()
    void Start()
    {
        _enemyPool = ActorManager.Instance.GetActorPool(Enums.ActorType.DefaultEnemy);

        //var destinations = GameObject.FindGameObjectsWithTag(GameTags.Destination);
        //_destinations = new Transform[destinations.Length];
        //Debug.Log("Creating destinations = " + _destinations.Length);
        //for (int i = 0; i < destinations.Length; i++)
        //{
        //    _destinations[i] = destinations[i].transform;
        //}

    }

    void Update()
    {
        _currentCooldown -= Time.deltaTime;

        if (_currentCooldown <= 0f)
        {
            _currentCooldown = _spawnRate;

            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        //Transform destination = ComputeDestination();

        GameObject go = _enemyPool.GetPooledObject();
        go.transform.position = transform.position;
        go.SetActive(true);

        EnemyController controller = go.GetComponent<EnemyController>();
        //controller.Initialize(destination);
        controller.Initialize();

        EnemyProperties properties = go.GetComponent<EnemyProperties>();
        OnEnemySpawned(new EnemyPropertiesEventArgs(go, properties));
    }

    //Transform ComputeDestination()
    //{
    //    return ComputeClosestDestination();
    //}

    //Transform ComputeClosestDestination()
    //{
    //    Vector3 spawnPos = transform.position;
    //    Transform resultDestination = transform;
    //    float currentDistance = float.PositiveInfinity;
    //    foreach (Transform currentDestination in _destinations)
    //    {
    //        float distance = Vector3.Distance(spawnPos, currentDestination.position);
    //        if (distance <= currentDistance)
    //        {
    //            currentDistance = distance;
    //            resultDestination = currentDestination;
    //        }
    //    }

    //    return resultDestination;
    //}

    protected void OnEnemySpawned(EnemyPropertiesEventArgs e)
    {
        if (EnemySpawned != null)
        {
            EnemySpawned(this, e);
        }
    }

}
