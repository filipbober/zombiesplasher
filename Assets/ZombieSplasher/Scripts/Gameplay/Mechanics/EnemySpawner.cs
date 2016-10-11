using UnityEngine;
using System.Collections;
using System;

public class EnemySpawner : MonoBehaviour
{
    public static event System.EventHandler<ActorPropertiesEventArgs> ActorSpawned;

    [SerializeField]
    private Enums.ActorType _actorType;

    [SerializeField]
    float _spawnRate;

    private ActorManager _actorManager;

    float _currentCooldown;

    void Start()
    {
        _actorManager = ActorManager.Instance;
    }

    void Update()
    {
        _currentCooldown -= Time.deltaTime;

        // TODO: Make it coroutine
        if (_currentCooldown <= 0f)
        {
            _currentCooldown = _spawnRate;

            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        GameObject go = _actorManager.GetActorObj(_actorType);
        go.transform.position = transform.position;

        EnemyController controller = go.GetComponent<EnemyController>();
        controller.Initialize();

        ActorProperties properties = go.GetComponent<ActorProperties>();
        OnActorSpawned(new ActorPropertiesEventArgs(go, properties));
    }

    protected void OnActorSpawned(ActorPropertiesEventArgs e)
    {
        if (ActorSpawned != null)
        {
            ActorSpawned(this, e);
        }
    }
}
