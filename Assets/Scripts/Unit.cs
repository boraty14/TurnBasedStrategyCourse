using System;
using Grid;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private GridPosition _gridPosition;
    private MoveAction _moveAction;

    private void Awake()
    {
        _moveAction = GetComponent<MoveAction>();
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
    public GridPosition GetGridPosition() => _gridPosition;

}