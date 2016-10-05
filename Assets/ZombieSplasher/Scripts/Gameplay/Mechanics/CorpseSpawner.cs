using System;
using EventSystem;
using UnityEngine;

//public class CorpseSpawner : MonoBehaviour
public class CorpseSpawner : EventSystem.EventHandler
{
    [SerializeField]
    private Enums.ActorType _actorType;

    private ActorManager _actorManager;

    void Start()
    {
        _actorManager = ActorManager.Instance;
    }

    //void OnEnable()
    //{
    //    EnemyController.EnemyDown += SpawnEnemyCorpse;
    //}

    //void OnDisable()
    //{
    //    EnemyController.EnemyDown -= SpawnEnemyCorpse;
    //}

    //protected void SpawnEnemyCorpse(object sneder, ActorPropertiesEventArgs e)
    //{
    //    GameObject go = _actorManager.GetActorObj(_actorType);

    //    go.transform.position = e.EnemyGameObj.transform.position;
    //    go.transform.rotation = e.EnemyGameObj.transform.rotation;

    //    go.SetActive(true);
    //}

    protected void SpawnEnemyCorpse(ActorPropertiesEvent e)
    {
        GameObject go = _actorManager.GetActorObj(_actorType);

        go.transform.position = e.EnemyGameObj.transform.position;
        go.transform.rotation = e.EnemyGameObj.transform.rotation;

        go.SetActive(true);
    }

    public override void SubscribeEvents()
    {
        //throw new NotImplementedException();
        EventSystem.EventManager.Instance.AddListener<ActorPropertiesEvent>(SpawnEnemyCorpse);
    }

    public override void UnsubscribeEvents()
    {
        //throw new NotImplementedException();
        EventSystem.EventManager.Instance.RemoveListener<ActorPropertiesEvent>(SpawnEnemyCorpse);
    }
}
