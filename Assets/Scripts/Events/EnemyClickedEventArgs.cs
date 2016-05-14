using UnityEngine;
using System.Collections;

public class EnemyClickedEventArgs : System.EventArgs
{
    public GameObject GameObj { get; set; }
    public EnemyData EnemyData;

    public EnemyClickedEventArgs(GameObject go, EnemyData enemyData)
    {
        GameObj = go;
        EnemyData = enemyData;
    }
}
