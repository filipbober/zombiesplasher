using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private ObjectPooler _enemyPool;

    [SerializeField]
    private Transform[] _destinations;

    [SerializeField]
    float _spawnRate;

    float _currentCooldown;

    void Awake()
    {
        _currentCooldown = 0f;
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
        Transform destination = ComputeDestination();
        Transform returnDestination = transform;
        float proximityRadius = 0.1f;

        GameObject go = _enemyPool.GetPooledObject();
        go.transform.position = transform.position;
        go.SetActive(true);        

        EnemyController controller = go.GetComponent<EnemyController>();
        controller.Initialize(destination, returnDestination, proximityRadius);
    }

    Transform ComputeDestination()
    {
        return ComputeClosestDestination();
    }

    Transform ComputeClosestDestination()
    {
        Vector3 spawnPos = transform.position;
        Transform resultDestination = transform;
        float currentDistance = float.PositiveInfinity;
        foreach (Transform currentDestination in _destinations)
        {
            float distance = Vector3.Distance(spawnPos, currentDestination.position);
            if (distance <= currentDistance)
            {
                currentDistance = distance;
                resultDestination = currentDestination;
            }
        }

        return resultDestination;
    }

    Transform ComputeRandomDestination()
    {
        return _destinations[Random.Range(0, _destinations.Length)];
    }

}
