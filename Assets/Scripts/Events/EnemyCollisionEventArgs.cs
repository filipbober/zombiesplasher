using UnityEngine;
using System.Collections;

public class EnemyCollisionEventArgs : System.EventArgs
{
    public GameObject GameObj { get; set; }
    public EnemyData EnemyData;
    public Collider Other;


    public EnemyCollisionEventArgs(GameObject go, EnemyData enemyData, Collider other)
    {
        GameObj = go;
        EnemyData = enemyData;
        Other = other;
    }
}
