using UnityEngine;
using System.Collections;

public interface IEnemyMover
{
    void Initialize(Vector3 destinationPos, float speed);
    void SetDestination(Vector3 destinationPos);
    void SetSpeed(float speed);
}
