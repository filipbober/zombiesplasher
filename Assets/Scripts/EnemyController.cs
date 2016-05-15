using UnityEngine;
using System.Collections;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public static event System.EventHandler<EnemyPropertiesEventArgs> EnemyDown;

    private EnemyProperties _properties;
    private IEnemyMover _mover;
    private IEnemyInputResponse _inputResponse;
    private Seeker _seeker;

    private Transform _destination;
    private Path _path;

    private Vector3 _currentWaypoint;
    private int _currentWaypointNo = 0;

    void Awake()
    {
        _properties = GetComponent<EnemyProperties>();
        _mover = GetComponent<IEnemyMover>();
        _inputResponse = GetComponent<IEnemyInputResponse>();
        _seeker = GetComponent<Seeker>();

        _inputResponse.Initialize(_properties);
    }

    void OnEnable()
    {
        _inputResponse.EnemyClicked += EnemyClicked;
    }

    void OnDisable()
    {
        _inputResponse.EnemyClicked -= EnemyClicked;
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

    protected void EnemyClicked(object sender, EnemyPropertiesEventArgs e)
    {
        if (e.GameObj == gameObject)
        {
            OnEnemyDown(new EnemyPropertiesEventArgs(gameObject, _properties));
            gameObject.SetActive(false);
        }
    }

    protected void OnEnemyDown(EnemyPropertiesEventArgs e)
    {
        if (EnemyDown != null)
        {
            EnemyDown(this, e);
        }
    }

}
