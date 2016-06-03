using UnityEngine;
using System.Collections;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public static event System.EventHandler<EnemyPropertiesEventArgs> EnemyDown;
    public static event System.EventHandler<EnemyPropertiesEventArgs> DestinationWasReached;

    private EnemyProperties _properties;
    private IEnemyMover _mover;
    private IEnemyInputResponse _inputResponse;
    private Seeker _seeker;
    private EnemyPhysicsEvents _physicsEvents;

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
        _physicsEvents = GetComponent<EnemyPhysicsEvents>();

        _inputResponse.Initialize(_properties);
        _physicsEvents.Initialize(_properties);

    }

    void OnEnable()
    {
        _inputResponse.EnemyClicked += EnemyClicked;
        _physicsEvents.DestinationReached += DestinationReached;
    }

    void OnDisable()
    {
        _inputResponse.EnemyClicked -= EnemyClicked;
        _physicsEvents.DestinationReached -= DestinationReached;
    }

    void Update()
    {
        UpdatePathfinding();

        //TempUpdate();
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
            //gameObject.SetActive(false);
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
        if (e.EnemyGameObj == gameObject)
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

    protected void OnDestinationWasReached(EnemyPropertiesEventArgs e)
    {
        if (DestinationWasReached != null)
        {
            DestinationWasReached(this, e);
        }
    }

    protected void DestinationReached(object sender, EnemyPropertiesEventArgs e)
    {
        OnDestinationWasReached(e);
        gameObject.SetActive(false);
    }

    // TODO: Temp - remove
    //private float _collisionRecalculateCooldown = 3f;
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (_collisionRecalculateCooldown <= 0)
    //    {
    //        Debug.Log("Recalculating collision");
    //        _seeker.StartPath(transform.position, _destination.position, OnPathComplete);

    //        _collisionRecalculateCooldown = 3f;
    //    }
    //}

    //void TempUpdate()
    //{
    //    _collisionRecalculateCooldown -= Time.deltaTime;
    //}


    // Isometric
    // Add -Y of the feet coordinate to the order layer
    // to the whole sprite.
    // Things like dragon tail can have a special modifier script
    // so the tail is either on top or below the character

    // --

}
