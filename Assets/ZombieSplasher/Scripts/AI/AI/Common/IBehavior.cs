using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FCB.AI.BehaviorTree.Common
{
    public interface IBehavior
    {
        void OnEnter(BehaviorState state);
        NodeStatus OnBehave(BehaviorState state);
        void OnExit(BehaviorState state);
    }
}
