using FCB.AI.BehaviorTree.Common;

namespace FCB.AI.BehaviorTree.Composites
{
    /// <summary>
    /// OR
    /// </summary>
    public class Selector : Composite
    {
        private int _currentChild;

        public Selector(string name, params Node[] nodes) : base(name, nodes)
        {            
        }

        public override NodeStatus OnBehave(BehaviorState state)
        {
            while (_currentChild < Children.Count)
            {
                var status = Children[_currentChild].Behave(state);

                if (status != NodeStatus.Failure)
                    return status;

                _currentChild++;                    
            }

            return NodeStatus.Failure;
        }

        public override void OnEnter(BehaviorState state)
        {
            _currentChild = 0;
        }        
    }
}