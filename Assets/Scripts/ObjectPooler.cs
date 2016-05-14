using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{

    public static ObjectPooler CurrentInstance;
    public GameObject PooledObject;
    public int PooledAmount = 20;
    public bool WillGrow = true;


    private List<GameObject> _pooledObjects;

    //---------------------------------------------------------------------
    // Public methods
    //--------------------------------------------------------------------- 
    public GameObject GetPooledObject()
    {
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
        if (WillGrow)
        {
            GameObject obj = (GameObject)Instantiate(PooledObject);
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
        // Create a pool of objects.
        _pooledObjects = new List<GameObject>();
        for (int i = 0; i < PooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(PooledObject);
            obj.transform.SetParent(transform);
            _pooledObjects.Add(obj);
            obj.SetActive(false);

        }
    }
}
