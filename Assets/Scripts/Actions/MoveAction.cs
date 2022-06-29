using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Actions
{
    public class MoveAction : BaseAction
    {
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private const float MoveSpeed = 4f;
        private const float RotateSpeed = 10f;
        private const float StoppingDistance = .1f;

        [SerializeField] private int _maxMoveDistance = 3;
        [SerializeField] private Animator _unitAnimator;
        private Vector3 _targetPosition;

        private List<GridPosition> _validGridPositionList = new List<GridPosition>();


        protected override void Awake()
        {
            base.Awake();
            _targetPosition = transform.position;
        }

        private void Update()
        {
            if (!_isActive) return;

            Vector3 moveDirection = (_targetPosition - transform.position).normalized;
            float deltaTime = Time.deltaTime;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, deltaTime * RotateSpeed);

            if (Vector3.Distance(_targetPosition, transform.position) < StoppingDistance)
            {
                _unitAnimator.SetBool(IsWalking, false);
                _isActive = false;
                _onActionComplete?.Invoke();
            }
            else
            {
                _unitAnimator.SetBool(IsWalking, true);
                transform.position += moveDirection * deltaTime * MoveSpeed;
            }
        }

        public void Move(GridPosition gridPosition,Action clearBusy)
        {
            _targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
            _isActive = true;
            _onActionComplete = clearBusy;
        }

        public bool IsValidActionGridPosition(GridPosition gridPosition)
        {
            var validActionGridPosition = GetValidActionGridPositionList();
            return validActionGridPosition.Contains(gridPosition);
        }

        public List<GridPosition> GetValidActionGridPositionList()
        {
            _validGridPositionList.Clear();
            GridPosition unitGridPosition = _unit.GetGridPosition();

            for (int x = -_maxMoveDistance + 1; x < _maxMoveDistance; x++)
            {
                for (int z = -_maxMoveDistance + 1; z < _maxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition) ||
                        testGridPosition == unitGridPosition ||
                        LevelGrid.Instance.HasUnitOnGridPosition(testGridPosition)) continue;

                    _validGridPositionList.Add(testGridPosition);
                    Debug.Log(testGridPosition);
                }
            }


            return _validGridPositionList;
        }
    }
}