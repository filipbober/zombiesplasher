// Copyright (C) 2016 Filip Cyrus Bober

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace ZombieSplasher
{
    public class ActorLookup : MonoBehaviour
    {
        public Dictionary<Enums.ActorType, GameObject> ActorsGameObjects = new Dictionary<Enums.ActorType, GameObject>();
        public Dictionary<Enums.ActorType, ActorProperties> ActorsProperties = new Dictionary<Enums.ActorType, ActorProperties>();

        [SerializeField]
        private List<GameObject> _actors;

        void Awake()
        {
            foreach (var actor in _actors)
            {
                ActorProperties properties = actor.GetComponent<ActorProperties>();

                GameObject newActor;
                if (!ActorsGameObjects.TryGetValue(properties.ActorType, out newActor))
                {
                    ActorsGameObjects.Add(properties.ActorType, actor);
                }

                ActorProperties newProperties;
                if (!ActorsProperties.TryGetValue(properties.ActorType, out newProperties))
                {
                    ActorsProperties.Add(properties.ActorType, properties);
                }

            }
        }

    }
}
