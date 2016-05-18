using UnityEngine;
using System.Collections;
using System;

public class EnemyMover : MonoBehaviour, IEnemyMover
{
    private Vector3 _destinationPos;
    private float _speed;

    public void Initialize(Vector3 destinationPos, float speed)
    {
        _destinationPos = destinationPos;
        _speed = speed;
    }

    public void SetDestination(Vector3 destinationPos)
    {
        _destinationPos = destinationPos;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    void Update()
    {        
        Vector3 heading = _destinationPos - transform.position;
        transform.Translate(heading.normalized * _speed * Time.deltaTime);
    }
}
