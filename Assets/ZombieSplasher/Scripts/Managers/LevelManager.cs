using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int _startingLives;
    private int _livesLeft;

    [SerializeField]
    private int _startingDefaultEnemies;
    private int _defaultEnemiesLeft;

    void Awake()
    {
        _livesLeft = _startingLives;
        _defaultEnemiesLeft = _startingDefaultEnemies;
    }

    void OnEnable()
    {
        // Subscribe to GameManager events
        GameManager.LiveLostNotification += LiveLost;
        GameManager.EnemySpawnedNotification += EnemySpawned;
    }

    void OnDisable()
    {
        GameManager.LiveLostNotification -= LiveLost;
        GameManager.EnemySpawnedNotification -= EnemySpawned;
    }

    void EnemySpawned(object sender, ActorPropertiesEventArgs e)
    {
        Debug.Log("LevelManager -> EnemySpawned()");
    }

    void LiveLost()
    {
        Debug.Log("LevelManager -> LiveLost()");
    }

    
}
