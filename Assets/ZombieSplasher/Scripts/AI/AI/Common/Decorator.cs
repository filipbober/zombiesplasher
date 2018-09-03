using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FCB.AI.BehaviorTree.Common
{
    public class Decorator : Node
    {
        protected Node Child;

        public Decorator(string name, Node child) : base(name)
        {
            Child = child;
            Name = name;
        }
    }
}