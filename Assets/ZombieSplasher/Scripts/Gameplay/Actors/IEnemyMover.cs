// Copyright (C) 2016 Filip Cyrus Bober

using UnityEngine;

namespace ZombieSplasher
{
    public interface IEnemyMover
    {
        float DefaultSpeed { get; }
        void Initialize(Vector3 destinationPos, float speed, float rotationSpeed);
        void SetDestination(Vector3 destinationPos);
        void SetSpeed(float speed);
    }
}
