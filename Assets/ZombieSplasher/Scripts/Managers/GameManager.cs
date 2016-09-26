using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public static event System.EventHandler<EnemyPropertiesEventArgs> EnemySpawnedNotification;
    public static event System.EventHandler<EnemyPropertiesEventArgs> EnemyDownNotification;
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
        EnemySpawner.EnemySpawned += EnemySpawned;
        EnemyController.DestinationWasReached += LiveLost;
    }

    void OnDisable()
    {
        EnemyController.EnemyDown -= EnemyDown;
        EnemySpawner.EnemySpawned -= EnemySpawned;
        EnemyController.DestinationWasReached -= LiveLost;
    }

    void EnemyDown(object sender, EnemyPropertiesEventArgs e)
    {
        // UI -> subtract ZombiesLeft

        // LevelManager -> Notify zombie is dead

        // LevelManager -> Notify zombie is spawned
        OnEnemyDown(e);

    }

    void EnemySpawned(object sender, EnemyPropertiesEventArgs e)
    {
        OnEnemySpawned(e);
    }

    void LiveLost(object sender, EnemyPropertiesEventArgs e)
    {
        OnLiveLost();
    }

    protected void OnEnemySpawned(EnemyPropertiesEventArgs e)
    {
        if (EnemySpawnedNotification != null)
        {
            EnemySpawnedNotification(this, e);
        }
    }

    protected void OnEnemyDown(EnemyPropertiesEventArgs e)
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
