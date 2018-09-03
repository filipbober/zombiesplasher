using System.Collections.Generic;

namespace FCB.AI.BehaviorTree.Common
{
    public abstract class Composite : Node
    {
        protected List<Node> Children = new List<Node>();

        public Composite(string name, params Node[] nodes) : base(name)
        {
            Children.AddRange(nodes);
        }

        public override NodeStatus Behave(BehaviorState state)
        {
            return base.Behave(state);
        }
    }
}
