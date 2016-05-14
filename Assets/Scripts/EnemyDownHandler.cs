using UnityEngine;
using System.Collections;

public class EnemyDownHandler : MonoBehaviour
{
    public ObjectPooler EnemyCorpsePool;

    void OnEnable()
    {
        EnemyController.EnemyDown += SpawnEnemyCorpse;
    }

    void OnDisable()
    {
        EnemyController.EnemyDown -= SpawnEnemyCorpse;
    }

    void SpawnEnemyCorpse(object sender, EnemyDownEventArgs e)
    {
        GameObject go = EnemyCorpsePool.GetPooledObject();
        go.transform.position = e.GameObj.transform.position;

        DeadEnemyController controller = go.GetComponent<DeadEnemyController>();
        controller.Initialize(e.Destination, e.EnemyData);

        //IEnemyMover mover = go.GetComponent<IEnemyMover>();
        //mover.SetDestination(_returnDestination);
        //mover.Initialize(null, e.Destination, e.EnemyData.Speed);
        go.SetActive(true);
    }

}
