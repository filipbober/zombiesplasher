using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public static event System.EventHandler<ActorPropertiesEventArgs> EnemySpawnedNotification;
    public static event System.EventHandler<ActorPropertiesEventArgs> EnemyDownNotification;
    public static event System.Action LiveLostNotification;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    void OnEnable()
    {
        EnemyController.EnemyDown += EnemyDown;
        EnemySpawner.ActorSpawned += EnemySpawned;
        EnemyController.DestinationWasReached += LiveLost;
    }

    void OnDisable()
    {
        EnemyController.EnemyDown -= EnemyDown;
        EnemySpawner.ActorSpawned -= EnemySpawned;
        EnemyController.DestinationWasReached -= LiveLost;
    }

    void EnemyDown(object sender, ActorPropertiesEventArgs e)
    {
        // UI -> subtract ZombiesLeft

        // LevelManager -> Notify zombie is dead

        // LevelManager -> Notify zombie is spawned
        OnEnemyDown(e);

    }

    void EnemySpawned(object sender, ActorPropertiesEventArgs e)
    {
        OnEnemySpawned(e);
    }

    void LiveLost(object sender, ActorPropertiesEventArgs e)
    {
        OnLiveLost();
    }

    protected void OnEnemySpawned(ActorPropertiesEventArgs e)
    {
        if (EnemySpawnedNotification != null)
        {
            EnemySpawnedNotification(this, e);
        }
    }

    protected void OnEnemyDown(ActorPropertiesEventArgs e)
    {
        if (EnemyDownNotification != null)
        {
            EnemyDownNotification(this, e);
        }
    }

    protected void OnLiveLost()
    {
        if (LiveLostNotification != null)
        {
            LiveLostNotification();
        }
    }


}
