using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

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
    }

    void OnDisable()
    {
        EnemyController.EnemyDown -= EnemyDown;
    }

    void EnemyDown(object sneder, EnemyPropertiesEventArgs e)
    {
        // UI -> subtract ZombiesLeft
        
    }

}
