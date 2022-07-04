using System;
using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    [SerializeField] private GridDebugObject gridDebugObjectPrefab;
    private GridSystem _gridSystem;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Level Grid");
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _gridSystem = new GridSystem(10, 10, 2f);
        _gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }

    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }

    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    public void UnitMovedGridPosition(Unit unit, GridPosition from, GridPosition to)
    {
        RemoveUnitAtGridPosition(from, unit);
        AddUnitAtGridPosition(to, unit);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => _gridSystem.GetGridPosition(worldPosition);
    public Vector3 GetWorldPosition(GridPosition gridPosition) => _gridSystem.GetWorldPosition(gridPosition);
    public bool IsValidGridPosition(GridPosition gridPosition) => _gridSystem.IsValidGridPosition(gridPosition);

    public int GetWidth() => _gridSystem.GetWidth();
    public int GetHeight() => _gridSystem.GetHeight();
    
    public bool HasUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
        return gridObject.HasAnyUnit();
    }
    
    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnit();
    }

}