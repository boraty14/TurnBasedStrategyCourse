using System;
using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private const float MoveSpeed = 4f;
    private const float RotateSpeed = 10f;
    private const float StoppingDistance = .1f;

    [SerializeField] private int _maxMoveDistance = 4;
    [SerializeField] private Animator _unitAnimator;
    private Unit _unit;
    private Vector3 _targetPosition;


    private void Awake()
    {
        _targetPosition = transform.position;
        _unit = GetComponent<Unit>();
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
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, deltaTime * RotateSpeed);
        transform.position += moveDirection * deltaTime * MoveSpeed;
    }

    public void Move(GridPosition gridPosition)
    {
        _targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        var validActionGridPosition = GetValidActionGridPositionList();
        return validActionGridPosition.Contains(gridPosition);
    }

    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = _unit.GetGridPosition();

        for (int x = -_maxMoveDistance; x <= _maxMoveDistance; x++)
        {
            for (int z = -_maxMoveDistance; z < _maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition) ||
                    testGridPosition == unitGridPosition ||
                    LevelGrid.Instance.HasUnitOnGridPosition(testGridPosition)) continue;
                
                validGridPositionList.Add(testGridPosition);
                Debug.Log(testGridPosition);
            }
        }


        return validGridPositionList;
    }
}