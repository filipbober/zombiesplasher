using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public enum AiState { Explore, Move, Interact, Wander };
    public enum Interaction { Attack, Trade, Loot, Repair };

    public class Utility
    {
        public Interaction Type;
        public float Value;

        public Utility(Interaction type, float value)
        {
            Type = type;
            Value = value;
        }
    }

    public class Decision
    {
        public readonly AiState State;
        public InteractableObject ObjectOfInterest;

        public Decision(AiState state, InteractableObject objectOfInterest)
        {
            State = state;
            ObjectOfInterest = objectOfInterest;
        }
    }

    public class Brain : MonoBehaviour
    {
        private SharedMemory _sharedMemory;
        private Memory _memory;
        private ClassFeatures _classFeatures;

        // Use this for initialization
        void Start()
        {
            _sharedMemory = SharedMemory.Instance;
            _memory = GetComponent<Memory>();
            _classFeatures = GetComponent<ClassFeatures>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ComputeDecision()
        {
            double bestUtility = 0.0;
            InteractableObject chosenInterest = null;

            foreach (var interest in _sharedMemory.InteractableObjects)
            {

            }

        }
    }
}
