using UnityEngine;

namespace FCB.AI.BehaviorTree.Common
{
    //https://github.com/GrymmyD/UnityBehaviourTree/blob/master/Primitives.cs
    // todo: Implement composite node, leaf node and (optional) decorator node
    // TODO: Check debug name - maybe set in base constructor
    public abstract class Node : IBehavior
    {
        public bool ShowDebug = false;
        protected string Name;

        private NodeStatus _status = NodeStatus.Invalid;

        public Node(string name) { }

        public virtual NodeStatus Behave(BehaviorState state)
        {
            // TODO: Checking conditionals every tick hits performance
            if (_status != NodeStatus.Running)
                OnEnter(state);

            if (ShowDebug)
                Debug.Log("OnBehave " + Name);

            _status = OnBehave(state);

            if (_status != NodeStatus.Running)
                OnExit(state);

            if (ShowDebug)
                Debug.Log("Return " + Name + " = " + _status.ToString());

            return _status;
        }

        public virtual NodeStatus OnBehave(BehaviorState state)
        {
            Debug.LogWarning("Not implemented");
            return NodeStatus.Invalid;
        }

        public virtual void OnEnter(BehaviorState state)
        {
            if (ShowDebug)
                Debug.Log("OnEnter " + Name);

            Debug.LogWarning("Not implemented");
        }

        public virtual void OnExit(BehaviorState state)
        {
            if (ShowDebug)
                Debug.Log("OnExit " + Name);

            Debug.LogWarning("Not implemented");
        }
    }
}
