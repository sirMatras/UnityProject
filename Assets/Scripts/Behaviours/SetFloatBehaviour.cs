using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFloatBehaviour : StateMachineBehaviour
{
    public string floatName;

    public bool updateOnStateEnter, updateOnStaterExit;

    public bool updateOnStateMachineEnter, updateOnStateMachineExit;

    public float valueOnEnter, valueOnExit;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateEnter)
        {
            animator.SetFloat(floatName, valueOnEnter);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStaterExit)
        {
            animator.SetFloat(floatName, valueOnExit);
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachineEnter)
        {
            animator.SetFloat(floatName, valueOnEnter);
        }
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachineExit)
        {
            animator.SetFloat(floatName, valueOnExit);
        }
    }
}
