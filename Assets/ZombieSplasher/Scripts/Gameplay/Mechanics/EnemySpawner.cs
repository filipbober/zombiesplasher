using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public static event System.EventHandler<EnemyPropertiesEventArgs> EnemySpawned;

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
        go.SetActive(true);

        EnemyController controller = go.GetComponent<EnemyController>();
        controller.Initialize();

        EnemyProperties properties = go.GetComponent<EnemyProperties>();
        OnEnemySpawned(new EnemyPropertiesEventArgs(go, properties));
    }

    protected void OnEnemySpawned(EnemyPropertiesEventArgs e)
    {
        if (EnemySpawned != null)
        {
            EnemySpawned(this, e);
        }
    }

}
