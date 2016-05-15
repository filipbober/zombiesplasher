using UnityEngine;
using System.Collections;

public class EnemyPropertiesEventArgs : System.EventArgs
{

    public GameObject GameObj { get; set; }
    public EnemyProperties EnemyProperties;

    public EnemyPropertiesEventArgs(GameObject go, EnemyProperties enemyProperties)
    {
        GameObj = go;
        EnemyProperties = enemyProperties;
    }
}
