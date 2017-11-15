using FCB.AI.BehaviorTree.Common;

using Pathfinding;
using UnityEngine;

namespace FCB.AI.BehaviorTree.Leaves
{
    public class Move : Leaf
    {
        private Path _currentPath = null;
        private int _waypoint = 0;
        private int _ticks = 0;

        public Move(string name) : base(name)
        {
        }

        public override NodeStatus OnBehave(BehaviorState state)
        {
            Context context = (Context)state;            

            if (context.Target == null)
                return NodeStatus.Failure;

            if (AtDestination(context))
                return NodeStatus.Success;

            if (_currentPath != null)
            {
                // Move for 120 ticks max (2s)
                if (_ticks > 120)
                {
                    // context.Self.LookAt(context.Target)
                    return NodeStatus.Success;
                }

                if (_waypoint >= _currentPath.vectorPath.Count)
                {
                    return NodeStatus.Success;
                }

                //Vector3 pathPoint = _currentPath.vectorPath[_waypoint];
                //if (Vector3.Distance(context.Self.transform.position, pathPoint) < 0.5f)
                //{
                //    _waypoint++;
                //}
                //else
                //{
                //    //var dir = pathPoint - context.Self.transform.position;
                //    //context.Self.transform.position += dir * Time.deltaTime;
                //    context.Mover.SetDestination(_currentPath.vectorPath[_waypoint]);
                //}
            }
            
            return NodeStatus.Running;
        }

        public override void OnEnter(BehaviorState state)
        {
            _currentPath = null;
            _waypoint = 0;
            _ticks = 0;

            Context context = (Context)state;
            Debug.LogError("Enter!");
            context.Controller.Initialize();
            return;

            context.PathSeeker.StartPath(context.Self.transform.position, context.MoveTarget, (Path p) =>
            {
                _currentPath = p;
                _waypoint = 2;
            });

            //context.Mover.Initialize(context.MoveTarget, 5.0f, 1.0f);
        }

        private bool AtDestination(Context context)
        {
            return false;
        }
    }
}
