using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using Grid;
using UnityEngine;

public class ShootAction : BaseAction
{
    [SerializeField] private int _maxShootDistance = 7;

    private State _state;
    private float _stateTimer;
    private Unit _targetUnit;
    private bool _canShootBullet;

    private const float RotateSpeed = 10f;

    public Action onShoot;

    private enum State
    {
        Aiming,
        Shooting,
        CoolOff
    }

    private void Update()
    {
        if (!_isActive) return;

        _stateTimer -= Time.deltaTime;

        switch (_state)
        {
            case State.Aiming:
                var aimDirection = (_targetUnit.GetWorldPosition() - _unit.GetWorldPosition()).normalized;
                transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * RotateSpeed);
                break;
            case State.Shooting:
                if (_canShootBullet)
                {
                    Shoot();
                    _canShootBullet = false;
                }

                break;
            case State.CoolOff:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (_stateTimer < 0f) NextState();
    }


    private void Shoot()
    {
        onShoot?.Invoke();
        _targetUnit.Damage();
    }

    private void NextState()
    {
        switch (_state)
        {
            case State.Aiming:
                _state = State.Shooting;
                _stateTimer = .1f;
                break;
            case State.Shooting:
                _state = State.CoolOff;
                _stateTimer = .5f;
                break;
            case State.CoolOff:
                ActionComplete();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override string GetActionName()
    {
        return "Shoot";
    }

    public override void TakeAction(GridPosition gridPosition, Action clearBusy)
    {
        ActionStart(clearBusy);
        _targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
        _canShootBullet = true;
        _state = State.Aiming;
        _stateTimer = 1f;
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        _validGridPositionList.Clear();
        GridPosition unitGridPosition = _unit.GetGridPosition();

        for (int x = -_maxShootDistance + 1; x < _maxShootDistance; x++)
        {
            for (int z = -_maxShootDistance + 1; z < _maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition) ||
                    !LevelGrid.Instance.HasUnitOnGridPosition(testGridPosition))
                    continue;

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > _maxShootDistance) continue;

                Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);
                if (targetUnit.IsEnemy() == _unit.IsEnemy()) continue;

                _validGridPositionList.Add(testGridPosition);
            }
        }


        return _validGridPositionList;
    }
}