using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AI
{
    public class SharedMemory : MonoBehaviour
    {
        public static SharedMemory Instance = null;

        public List<InteractableObject> InteractableObjects;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}
