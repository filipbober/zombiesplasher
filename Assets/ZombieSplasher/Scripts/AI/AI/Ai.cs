using FCB.AI.BehaviorTree.Common;
using FCB.AI.BehaviorTree.Composites;
using FCB.AI.BehaviorTree.Leaves;
using UnityEngine;
using ZombieSplasher;

public class Ai : MonoBehaviour
{
    public GameObject Target;
    public Seeker Seeker;

    private Node _behaviorTree;
    Context _state;

    // Use this for initialization
    void Start()
    {
        _behaviorTree = CreateBehaviorTree();
        _state = new Context();
        _state.MoveTarget = Target.transform.position;
        _state.Self = gameObject;
        _state.Target = Target;
        _state.PathSeeker = GetComponent<Seeker>();
        _state.Mover = GetComponent<IEnemyMover>();
        _state.Controller = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _behaviorTree.Behave(_state);
    }

    private Node CreateBehaviorTree()
    {
        Selector selector = new Selector("test selector", new Move("test move"));

        return selector;
    }
}
