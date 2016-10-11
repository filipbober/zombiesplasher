using UnityEngine;
using Pathfinding;
using System;

public class EnemyController : FCB.EventSystem.EventHandler, IActorController
{
    public static event System.EventHandler<ActorPropertiesEventArgs> EnemyDown;
    public static event System.EventHandler<ActorPropertiesEventArgs> DestinationWasReached;

    private ActorProperties _actorProperties;
    private EnemyProperties _properties;
    private IEnemyMover _mover;
    private IActorInputResponse _inputResponse;
    private Seeker _seeker;
    private EnemyPhysicsEvents _physicsEvents;

    Transform[] _destinations;
    private Transform _destination;
    private Path _path;

    private Vector3 _currentWaypoint;
    private int _currentWaypointNo = 0;

    public override void SubscribeEvents()
    {
        //FCB.EventSystem.EventManager.Instance.AddListener<ActorClickedEvent>(OnEnemyClicked);
        _inputResponse.ActorClicked += EnemyClicked;

        // TODO: id should be Event owner id, not current gameobject (in this case it is the same)
        // Maybe implement interface ISingleEvent, which contains event and gameobject
        FCB.EventSystem.SingleEventManager.Instance.AddListener<ActorClickedEvent>(new FCB.EventSystem.SingleEvent(gameObject.GetInstanceID(), new ActorClickedEvent(gameObject, _actorProperties)), OnEnemyClicked);

        _physicsEvents.DestinationReached += DestinationReached;
    }

    public override void UnsubscribeEvents()
    {
        _inputResponse.ActorClicked -= EnemyClicked;
        //FCB.EventSystem.SingleEventManager.Instance.RemoveListener<ActorClickedEvent>(gameObject.GetInstanceID(), OnEnemyClicked);

        _physicsEvents.DestinationReached -= DestinationReached;
    }

    public void Initialize()
    {
        _destination = ComputeDestination();
        _seeker.StartPath(transform.position, _destination.position, OnPathComplete);

        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void Awake()
    {
        _actorProperties = GetComponent<ActorProperties>();
        _properties = GetComponent<EnemyProperties>();
        _mover = GetComponent<IEnemyMover>();
        _inputResponse = GetComponent<IActorInputResponse>();
        _seeker = GetComponent<Seeker>();
        _physicsEvents = GetComponent<EnemyPhysicsEvents>();

        _inputResponse.Initialize(_actorProperties);
        _physicsEvents.Initialize(_actorProperties);

        CreateDestinations(); 
    }

    void Update()
    {
        UpdatePathfinding();
    }

    public void OnPathComplete(Path path)
    {
        if (!path.error)
        {            
            _path = path;
            _currentWaypointNo = 0;

            _mover.Initialize(_currentWaypoint, _properties.Speed, _properties.RotationSpeed);
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

    protected void EnemyClicked(object sender, ActorPropertiesEventArgs e)
    //protected void OnEnemyClicked(ActorPropertiesEvent e)
    {
        if (e.Sender == gameObject)
        {
            //FCB.EventSystem.EventManager.Instance.Raise(new SpawnCorpseEvent(gameObject, _actorProperties));
            OnEnemyDown(new ActorPropertiesEventArgs(gameObject, _actorProperties));
            //gameObject.SetActive(false);
            Deactivate();            
        }
    }

    protected void OnEnemyClicked(ActorPropertiesEvent e)
    {
        Debug.Log("OnEnemyClicked, id =" + gameObject.GetInstanceID());
    }

    protected void OnEnemyDown(ActorPropertiesEventArgs e)
    {
        if (EnemyDown != null)
        {
            EnemyDown(this, e);
            //FCB.EventSystem.EventManager.Instance.Raise(new 
            FCB.EventSystem.EventManager.Instance.Raise(new SpawnCorpseEvent(e.Sender, e.ActorProperties));
        }
    }

    protected void OnDestinationWasReached(ActorPropertiesEventArgs e)
    {
        if (DestinationWasReached != null)
        {
            DestinationWasReached(this, e);
        }
    }

    protected void DestinationReached(object sender, ActorPropertiesEventArgs e)
    {
        OnDestinationWasReached(e);
        gameObject.SetActive(false);
    }

    protected Transform ComputeDestination()
    {
        return ComputeClosestDestination();
    }

    protected void CreateDestinations()
    {
        var destinations = GameObject.FindGameObjectsWithTag(GameTags.Destination);
        _destinations = new Transform[destinations.Length];
        
        //Debug.Log("Creating destinations = " + _destinations.Length);

        for (int i = 0; i < destinations.Length; i++)
        {
            _destinations[i] = destinations[i].transform;
        }
    }

    private Transform ComputeClosestDestination()
    {
        Vector3 spawnPos = transform.position;
        Transform resultDestination = transform;
        float currentDistance = float.PositiveInfinity;
        foreach (Transform currentDestination in _destinations)
        {
            float distance = Vector3.Distance(spawnPos, currentDestination.position);
            if (distance <= currentDistance)
            {
                currentDistance = distance;
                resultDestination = currentDestination;
            }
        }

        return resultDestination;
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
