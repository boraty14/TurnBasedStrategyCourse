using System;
using Actions;
using Grid;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private GridPosition _gridPosition;
    private MoveAction _moveAction;
    private SpinAction _spinAction;
    private BaseAction[] _baseActionArray;

    private int _actionPoints = 2;

    private void Awake()
    {
        _moveAction = GetComponent<MoveAction>();
        _spinAction = GetComponent<SpinAction>();
        _baseActionArray = GetComponents<BaseAction>();
    }

    private void Start()
    {
        _gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(_gridPosition,this);
    }

    private void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition == _gridPosition) return;
        LevelGrid.Instance.UnitMovedGridPosition(this,_gridPosition,newGridPosition);
        _gridPosition = newGridPosition;

    }

    public MoveAction GetMoveAction() => _moveAction;
    public SpinAction GetSpinAction() => _spinAction;
    public GridPosition GetGridPosition() => _gridPosition;
    public BaseAction[] GetBaseActionArray() => _baseActionArray;

    public bool TrySpendActionPointsToTakeAction(BaseAction action)
    {
        if (!CanSpendActionPointsToTakeAction(action)) return false;
        SpendActionPoints(action.GetActionPointsCost());
        return true;
    }
    private bool CanSpendActionPointsToTakeAction(BaseAction action) => _actionPoints >= action.GetActionPointsCost();
    private void SpendActionPoints(int amount) => _actionPoints -= amount;
    public int GetActionPoints() => _actionPoints;
}