using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator _unitAnimator;

    private Vector3 _targetPosition;
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");

    private const float MoveSpeed = 4f;
    private const float RotateSpeed = 10f;
    private const float StoppingDistance = .1f;

    private void Awake()
    {
        _targetPosition = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(_targetPosition, transform.position) < StoppingDistance)
        {
            _unitAnimator.SetBool(IsWalking, false);
            return;
        }

        _unitAnimator.SetBool(IsWalking, true);

        Vector3 moveDirection = (_targetPosition - transform.position).normalized;
        float deltaTime = Time.deltaTime;
        transform.forward = Vector3.Lerp(transform.forward,moveDirection,deltaTime * RotateSpeed);
        transform.position += moveDirection * deltaTime * MoveSpeed;
    }

    public void Move(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}