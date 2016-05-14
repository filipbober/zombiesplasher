using UnityEngine;
using System.Collections;
using System;

public class EnemyMover : MonoBehaviour, IEnemyMover
{
    private float _speed;

    [SerializeField]
    private float _proximityRadius;

    private NavMeshAgent _navAgent;

    private Transform _destination;

    public void Initialize(NavMeshAgent navAgent, Transform initialDestination, float speed)
    {
        _navAgent = navAgent;
        SetDestination(initialDestination);
        SetSpeed(speed);        
    }

    public void SetDestination(Transform destination)
    {
        _destination = destination;

        if (_navAgent != null)
        {
            _navAgent.SetDestination(destination.position);
        }
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;

        if (_navAgent != null)
        {
            _navAgent.speed = _speed;
        }
    }

    void Update()
    {
        //if (!_navAgent.isActiveAndEnabled)
        //{
        //    Vector3 heading = _destination.position - transform.position;
        //    //transform.Translate(heading.normalized * _speed * Time.deltaTime);

        //    Debug.Log("Destination = " + _destination.position);
        //    Debug.Log("Heading = " + heading.normalized);

        //    //GetComponent<Rigidbody>().MovePosition(heading.normalized * _speed * Time.deltaTime);
        //}

        if (_navAgent == null)
        {
            Vector3 heading = _destination.position - transform.position;
            transform.Translate(heading.normalized * _speed * Time.deltaTime);
        }
    }

}
