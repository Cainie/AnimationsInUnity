using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Animator _Animator;

    [SerializeField] private float _WalkSpeed = 0.2f;
    
    [SerializeField] private string _IdleTrigger;
    [SerializeField] private string _WalkTrigger;

    [SerializeField] private List<TriggerPair> _TriggerPairs;

    private Vector3 _Direction;

    private MovementStateMachine _StateMachine;
    private MovementStateMachine StateMachine
    {
        get
        {
            return _StateMachine ?? (_StateMachine = new MovementStateMachine(_Animator, _IdleTrigger, _WalkTrigger));
        }
    }

    private void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        var offset = new Vector2(vertical, horizontal);
        if (offset.magnitude > 1f)
        {
            offset.Normalize();
        }

        var dir = Camera.main.transform.rotation * (new Vector3(horizontal, 0f, vertical));
        dir.y = 0.0f;

        if (Math.Abs(vertical) < Mathf.Epsilon && Math.Abs(horizontal) < Mathf.Epsilon)
        {
            StateMachine.Idle();
        }
        else
        {
            _Direction = dir;

            dir *= Time.deltaTime * _WalkSpeed;
            StateMachine.Walk();
        }

        transform.Translate(dir, Space.World);

        foreach (var triggerPair in _TriggerPairs)
        {
            if (Input.GetKeyDown(triggerPair.Key))
            {
                StateMachine.Trigger(triggerPair.Name);
            }
        }
    }

    private void LateUpdate()
    {
        if (_Direction.magnitude > 0.0001f)
        {
            transform.rotation = Quaternion.LookRotation(_Direction);
        }
    }

    [Serializable]
    private class TriggerPair
    {
#pragma warning disable 649
        public KeyCode Key;

        public string Name;
#pragma warning restore 649
    }
}
