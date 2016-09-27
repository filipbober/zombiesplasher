using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{

    public static ObjectPooler CurrentInstance;

    [SerializeField]
    private GameObject _pooledObject;

    [SerializeField]
    private int _pooledAmount = 20;

    [SerializeField]
    private bool _willGrow = true;


    private List<GameObject> _pooledObjects;

    //---------------------------------------------------------------------
    // Public methods
    //--------------------------------------------------------------------- 
    public GameObject GetPooledObject()
    {
        if (_pooledObjects == null)
            InitializePool();

        // Find inactive pool object and return it. 
        // Setting that object to active is not the responsibility of this method.
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }

        // Expand the collection if it's too small and growing is permitted.
        if (_willGrow)
        {
            GameObject obj = (GameObject)Instantiate(_pooledObject);
            obj.transform.SetParent(transform);
            _pooledObjects.Add(obj);
            return obj;
        }

        // We are out of object and we are not allowed to create new ones.
        return null;

    }

    //---------------------------------------------------------------------
    // Mono protected methods
    //--------------------------------------------------------------------- 
    protected void Awake()
    {
        CurrentInstance = this;
    }

    protected void Start()
    {
        if (_pooledObjects == null)
            InitializePool();
    }

    private void InitializePool()
    {
        // Create a pool of objects.
        _pooledObjects = new List<GameObject>();
        for (int i = 0; i < _pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(_pooledObject);
            obj.transform.SetParent(transform);
            _pooledObjects.Add(obj);
            obj.SetActive(false);

        }
    }

}
