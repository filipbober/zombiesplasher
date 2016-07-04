using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameHUDManager : MonoBehaviour
{
    public Text LivesLeftText;
    public Text EnemiesLeftText;

    void OnEnable()
    {
        GameManager.EnemySpawnedNotification += UpdateEnemiesLeft;
    }

    void OnDisable()
    {

    }

    void UpdateEnemiesLeft(object sender, EnemyPropertiesEventArgs e)
    {
        EnemiesLeftText.text = "-1";
    }

}
