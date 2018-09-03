// Copyright (C) 2016 Filip Cyrus Bober

using UnityEngine;
using System;

namespace ZombieSplasher
{
    // TODO: Implement interface IEnemyPhysicsEvents
    public class EnemyPhysicsEvents : MonoBehaviour
    {
        public event EventHandler<ActorPropertiesEventArgs> DestinationReached;

        private ActorProperties _actorProperties;

        public void Initialize(ActorProperties actorProperties)
        {
            _actorProperties = actorProperties;
        }

        public void OnDestinationReached(ActorPropertiesEventArgs e)
        {
            if (DestinationReached != null)
            {
                DestinationReached(this, e);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            CheckDestinationEnter(other);
            CheckTerrainEnter(other);
        }

        void OnTriggerExit(Collider other)
        {
            CheckTerrainExit(other);
        }

        void CheckDestinationEnter(Collider other)
        {
            if (other.CompareTag(GameTags.Destination))
                OnDestinationReached(new ActorPropertiesEventArgs(gameObject, _actorProperties));
        }

        void CheckTerrainEnter(Collider other)
        {
            if (other.CompareTag(GameTags.Terrain))
            {
                var speedModifier = other.GetComponent<TerrainSettings>().SpeedModifier;
                FCB.EventSystem.SingleEventManager.Instance.Raise(gameObject.GetInstanceID(), new TerrainEnterEvent(gameObject, speedModifier));
            }
        }

        void CheckTerrainExit(Collider other)
        {
            if (other.CompareTag(GameTags.Terrain))
            {
                FCB.EventSystem.SingleEventManager.Instance.Raise(gameObject.GetInstanceID(), new TerrainExitEvent(gameObject));
            }
        }

    }
}
