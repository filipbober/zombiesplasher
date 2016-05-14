using UnityEngine;
using System.Collections;

public class EnemyDownEventArgs : System.EventArgs
{
    public GameObject GameObj;
    public EnemyData EnemyData;
    public Transform Destination;

    public EnemyDownEventArgs(GameObject go, EnemyData enemyData, Transform destination)
    {
        GameObj = go;
        EnemyData = enemyData;
        Destination = destination;
    }

}
