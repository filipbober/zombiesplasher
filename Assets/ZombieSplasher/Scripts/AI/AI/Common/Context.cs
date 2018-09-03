using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieSplasher;

namespace FCB.AI.BehaviorTree.Common
{
    public class Context : BehaviorState
    {
        // TODO: Sample members
        public Seeker PathSeeker;
        public GameObject Self;
        public GameObject Target;
        public Vector3 MoveTarget;
        public IEnemyMover Mover;
        public EnemyController Controller;
    }
}
