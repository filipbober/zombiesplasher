using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    //public static event 
    public static event System.EventHandler<EnemyDownEventArgs> EnemyDown;

    private IEnemyMover _mover;
    private IEnemyInputResponse _inputResponse;
    private IEnemyCollisionResponse _collisionResponse;
    private EnemyData _enemyData;
    private NavMeshAgent _navAgent;

    private Transform _destination;
    private Transform _returnDestination;
    private float _proximityRadius;

    private bool _isReturning = false;
    private Transform _currentDestination;

    public void Initialize(Transform destination, Transform returnDestination, float proximityRadius)
    {
        _destination = destination;
        _returnDestination = returnDestination;
        _proximityRadius = proximityRadius;
        float speed = Random.Range(_enemyData.Speed - _enemyData.SpeedFluctuation, _enemyData.Speed + _enemyData.SpeedFluctuation);

        _mover.Initialize(_navAgent, _destination, speed);

        _isReturning = false;
        _currentDestination = _destination;
    }

    void Awake()
    {
        _mover = GetComponent<IEnemyMover>();
        if (_mover == null)
        {
            Debug.LogError("IEnemyMover is null!");
        }

        _inputResponse = GetComponent<IEnemyInputResponse>();
        if (_inputResponse == null)
        {
            Debug.LogError("IEnemyInputResponse is null!");
        }

        _collisionResponse = GetComponent<IEnemyCollisionResponse>();
        if (_collisionResponse == null)
        {
            Debug.LogError("IEnemyCollisionResponse is null!");
        }

        _enemyData = GetComponent<EnemyData>();
        if (_enemyData == null)
        {
            Debug.LogError("EnemyData is null!");
        }

        _navAgent = GetComponent<NavMeshAgent>();
        if (_navAgent == null)
        {
            Debug.LogError("NavMeshAgent is null!");
        }

        _inputResponse.Initialize(_enemyData);
        _collisionResponse.Initialize(_enemyData);
    }

    public void SetDestination(Transform destination)
    {
        _currentDestination = destination;
        _mover.SetDestination(_currentDestination);
    }

    void OnEnable()
    {
        EventManager.StartListening("Test", EventTrigger);

        //EnemyInputResponse.EnemyClicked += EnemyClicked;

        //_navAgent.enabled = true;

        _inputResponse.EnemyClicked += EnemyClicked;
        _collisionResponse.EnemyCollided += EnemyCollided;
    }

    void OnDisable()
    {
        EventManager.StopListening("Test", EventTrigger);

        //EnemyInputResponse.EnemyClicked -= EnemyClicked;
        _inputResponse.EnemyClicked -= EnemyClicked;
        _collisionResponse.EnemyCollided -= EnemyCollided;
    }

    void Update()
    {
        //if (!_isReturning && IsDestinationReached())
        //{
        //    EventManager.TriggerEvent("Test");
        //    SetDestination(_returnDestination);
        //    _isReturning = true;
        //    Debug.Log("Life lost!");
        //    gameObject.SetActive(false);
        //}
        //else if (_isReturning && IsDestinationReached())
        //{
        //    gameObject.SetActive(false);
        //}

        //if (_isReturning && IsDestinationReached())
        //{
        //    gameObject.SetActive(false);
        //}

    }

    bool IsDestinationReached()
    {
        if (Vector3.Distance(transform.position, _currentDestination.position) < _proximityRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void EventTrigger()
    {
        Debug.Log("Capital reached, life lost!");
    }

    void DestinationReached()
    {

    }

    void EnemyClicked(object sender, EnemyClickedEventArgs e)
    {
        if (e.GameObj == gameObject)
        {
            SetDestination(_returnDestination);

            // ---

            //GameObject go = Instantiate(TempReturningObject);
            //go.transform.position = transform.position;
            //IEnemyMover mover = go.GetComponent<IEnemyMover>();
            //mover.Initialize(null, _returnDestination, _enemyData.Speed);
            //gameObject.SetActive(false);


            OnEnemyDown(new EnemyDownEventArgs(gameObject, _enemyData, _returnDestination));
            gameObject.SetActive(false);

            // ---

        }
    }

    void EnemyCollided(object sender, EnemyCollisionEventArgs e)
    {
        Debug.Log("Collision with capital!");
        if (e.GameObj == gameObject)
        {            
            gameObject.SetActive(false);
            Debug.Log("Destination reached!");
        }
    }

    protected void OnEnemyDown(EnemyDownEventArgs e)
    {
        if (EnemyDown != null)
        {
            EnemyDown(this, e);
        }
    }

}
