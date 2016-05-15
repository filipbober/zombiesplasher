using UnityEngine;
using System.Collections;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    private EnemyProperties _properties;
    private IEnemyMover _mover;
    private Seeker _seeker;

    private Transform _destination;
    private Path _path;

    private Vector3 _currentWaypoint;
    private int _currentWaypointNo = 0;

    void Awake()
    {
        _properties = GetComponent<EnemyProperties>();
        _mover = GetComponent<IEnemyMover>();
        _seeker = GetComponent<Seeker>();
    }

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    void Update()
    {
        UpdatePathfinding();
    }

    public void Initialize(Transform destination)
    {        
        _destination = destination;
        _seeker.StartPath(transform.position, _destination.position, OnPathComplete);
    }

    public void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            _path = path;
            _currentWaypointNo = 0;

            _mover.Initialize(_currentWaypoint, _properties.Speed);
            RefreshMover();
        }
    }

    bool IsWaypointReached(Vector3 waypoint)
    {
        if (Vector3.Distance(transform.position, _currentWaypoint) < _properties.ProximityRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void UpdatePathfinding()
    {
        if (_path == null)
        {
            //Debug.LogError("Path is null!");
            return;
        }

        if (_currentWaypointNo >= _path.vectorPath.Count)
        {
            Debug.Log("Destination reached!");
            gameObject.SetActive(false);
            return;
        }        

        if (IsWaypointReached(_currentWaypoint))
        {
            _currentWaypointNo++;
            //_currentWaypoint = _path.vectorPath[_currentWaypointNo];
            RefreshMover();        
        }

    }

    void RefreshMover()
    {
        if (_currentWaypointNo >= _path.vectorPath.Count)
            return;

        _currentWaypoint = _path.vectorPath[_currentWaypointNo];
        _mover.SetDestination(_currentWaypoint);
    }
}
