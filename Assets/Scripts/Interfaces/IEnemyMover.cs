using UnityEngine;
using System.Collections;

public interface IEnemyMover
{
    void Initialize(Vector3 destinationPos, float speed, float rotationSpeed);
    void SetDestination(Vector3 destinationPos);
    void SetSpeed(float speed);
}
