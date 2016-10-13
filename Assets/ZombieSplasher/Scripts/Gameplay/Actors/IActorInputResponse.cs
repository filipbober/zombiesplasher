// Copyright (C) 2016 Filip Cyrus Bober

using UnityEngine;
using System.Collections;

namespace ZombieSplasher
{
    public interface IActorInputResponse
    {
        event System.EventHandler<ActorPropertiesEventArgs> ActorClicked;

        void Initialize(ActorProperties enemyProperties);
        void OnActorClicked(ActorPropertiesEventArgs e);
    }
}
