﻿// Copyright (C) 2016 Filip Cyrus Bober

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
            if (other.CompareTag(GameTags.Destination))
            {
                OnDestinationReached(new ActorPropertiesEventArgs(gameObject, _actorProperties));
            }
        }
    }
}
