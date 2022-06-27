using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 _targetPosition;

    private const float MoveSpeed = 4f;
    private const float StoppingDistance = .1f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Move(MouseWorld.GetPosition());
        }

        if (Vector3.Distance(_targetPosition, transform.position) < StoppingDistance) return;

        Vector3 moveDirection = (_targetPosition - transform.position).normalized;
        transform.position += moveDirection * Time.deltaTime * MoveSpeed;
    }

    private void Move(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}