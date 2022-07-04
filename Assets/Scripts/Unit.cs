using System;
using Actions;
using Grid;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private bool _isEnemy;
    private GridPosition _gridPosition;
    private MoveAction _moveAction;
    private SpinAction _spinAction;
    private BaseAction[] _baseActionArray;

    private const int ACTION_POINTS_MAX = 2;
    private int _actionPoints = ACTION_POINTS_MAX;

    public static Action OnAnyActionPointsChanged;

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
        TurnSystem.Instance.OnTurnChanged += OnTurnChanged;
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
    private void SpendActionPoints(int amount)
    {
        _actionPoints -= amount;
        OnAnyActionPointsChanged?.Invoke();
    }

    public int GetActionPoints() => _actionPoints;
    public bool IsEnemy() => _isEnemy;
    private void OnTurnChanged()
    {
        if (!IsThisUnitsTurn()) return;
        _actionPoints = ACTION_POINTS_MAX;
        OnAnyActionPointsChanged?.Invoke();
    }

    private bool IsThisUnitsTurn()
    {
        return (IsEnemy() && !TurnSystem.Instance.IsPlayerTurn()) ||
               (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn());
    }

    public void Damage()
    {
        Debug.Log(transform.name + " damage");
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }
}