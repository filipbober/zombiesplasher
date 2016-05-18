using UnityEngine;
using System.Collections;

public class EnemyPropertiesEventArgs : System.EventArgs
{

    public GameObject EnemyGameObj { get; set; }
    public EnemyProperties EnemyProperties;

    public EnemyPropertiesEventArgs(GameObject enemyGameObj, EnemyProperties enemyProperties)
    {
        EnemyGameObj = enemyGameObj;
        EnemyProperties = enemyProperties;
    }
}
