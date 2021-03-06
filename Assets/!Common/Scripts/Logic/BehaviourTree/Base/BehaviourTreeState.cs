using System;
using Common.Logic;
using UnityEngine;

public class BehaviourTreeState : StateMachineBehaviour
{
    [SerializeField]
    private BehaviourTree tree;
    
    [NonSerialized]
    private BehaviourTreeController _controller;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _controller = animator.GetComponent<BehaviourTreeController>();
        if (_controller == null)
        {
            return;
        }
        if (tree == null)
        {
            return;
        }
        tree.root.Enter(_controller);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_controller == null)
        {
            return;
        }
        if (tree == null)
        {
            return;
        }
        tree.root.Evaluate(_controller);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_controller == null)
        {
            return;
        }
        if (tree == null)
        {
            return;
        }
        tree.root.Exit(_controller);
    }
}
