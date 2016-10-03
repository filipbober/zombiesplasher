using UnityEngine;

public class CorpseSpawner : MonoBehaviour
{
    [SerializeField]
    private Enums.ActorType _actorType;

    private ActorManager _actorManager;

    void Start()
    {
        _actorManager = ActorManager.Instance;
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
        GameObject go = _actorManager.GetActorObj(_actorType);

        go.transform.position = e.EnemyGameObj.transform.position;
        go.transform.rotation = e.EnemyGameObj.transform.rotation;

        go.SetActive(true);
    }

}
