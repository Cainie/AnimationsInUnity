using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateMachine : MonoBehaviour
{private readonly Animator _Animator;
    private readonly string _IdleTrigger;
    private readonly string _WalkTrigger;

    public MovementStateMachine(Animator animator, string idleTrigger, string walkTrigger)
    {
        _Animator = animator;
        _IdleTrigger = idleTrigger;
        _WalkTrigger = walkTrigger;
    }

    private State _CurrentState;

    private enum State
    {
        Idle, Walk
    }

    public void Trigger(string triggerName)
    {
        Debug.Log("Trigger: " + triggerName);
        _Animator.SetTrigger(triggerName);
    }

    public void Idle()
    {
        if (_CurrentState != State.Idle)
        {
            Debug.Log("Trigger: " + _IdleTrigger);
            _Animator.SetTrigger(_IdleTrigger);
            _CurrentState = State.Idle;
        }
    }

    public void Walk()
    {
        if (_CurrentState != State.Walk)
        {
            Debug.Log("Trigger: " + _WalkTrigger);
            _Animator.SetTrigger(_WalkTrigger);
            _CurrentState = State.Walk;
        }
    }
}
