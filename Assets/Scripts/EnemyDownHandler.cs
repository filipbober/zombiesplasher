using UnityEngine;
using System.Collections;

public class EnemyDownHandler : MonoBehaviour
{
    public ObjectPooler DefaultEnemyCorpsePool;

    void OnEnable()
    {
        EnemyController.EnemyDown += SpawnEnemyCorpse;
    }

    void OnDisable()
    {
        EnemyController.EnemyDown -= SpawnEnemyCorpse;
    }

    protected void SpawnEnemyCorpse(object sneder, EnemyPropertiesEventArgs e)
    {
        GameObject go = DefaultEnemyCorpsePool.GetPooledObject();
        go.transform.position = e.EnemyGameObj.transform.position;
        go.transform.rotation = e.EnemyGameObj.transform.rotation;

        //DeadEnemyController controller = go.GetComponent<DeadEnemyController>();
        //controller.Initialize(e.Destination, e.EnemyData);

        //IEnemyMover mover = go.GetComponent<IEnemyMover>();
        //mover.SetDestination(_returnDestination);
        //mover.Initialize(null, e.Destination, e.EnemyData.Speed);
        go.SetActive(true);
    }

}
