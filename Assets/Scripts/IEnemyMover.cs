using UnityEngine;
using System.Collections;

public interface IEnemyMover 
{
    void Initialize(NavMeshAgent navAgent, Transform initialDestination, float speed);
    void SetDestination(Transform destination);
    void SetSpeed(float speed);
}
