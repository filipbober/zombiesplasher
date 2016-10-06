using UnityEngine;

//public class CorpseSpawner : MonoBehaviour
public class CorpseSpawner : FCB.EventSystem.EventHandler
{
    [SerializeField]
    private Enums.ActorType _actorType;

    private ActorManager _actorManager;

    public override void SubscribeEvents()
    {
        FCB.EventSystem.EventManager.Instance.AddListener<SpawnCorpseEvent>(OnSpawnCorpse);
    }

    public override void UnsubscribeEvents()
    {
        FCB.EventSystem.EventManager.Instance.RemoveListener<SpawnCorpseEvent>(OnSpawnCorpse);
    }

    void Start()
    {
        _actorManager = ActorManager.Instance;
    }

    protected void OnSpawnCorpse(ActorPropertiesEvent e)
    {
        GameObject go = _actorManager.GetActorObj(_actorType);

        go.transform.position = e.Sender.transform.position;
        go.transform.rotation = e.Sender.transform.rotation;
        go.SetActive(true);
    }

    
}
