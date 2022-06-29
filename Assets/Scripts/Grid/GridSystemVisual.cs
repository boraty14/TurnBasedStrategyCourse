using System;
using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance { get; private set; }

    [SerializeField] private GridSystemVisualSingle _gridSystemVisualSinglePrefab;
    private GridSystemVisualSingle[,] _gridSystemVisualSingleArray;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one UnityAction System");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _gridSystemVisualSingleArray =
            new GridSystemVisualSingle[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];

        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                var gridSystemVisualSingle = Instantiate(_gridSystemVisualSinglePrefab,
                    LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
                _gridSystemVisualSingleArray[x, z] = gridSystemVisualSingle;
            }
        }
    }

    private void Update()
    {
        UpdateGridVisual();
    }

    public void HideAllGridPositions()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                _gridSystemVisualSingleArray[x, z].Hide();
            }
        }
    }

    public void ShowGridPositionList(List<GridPosition> gridPositionList)
    {
        for (int i = 0; i < gridPositionList.Count; i++)
        {
            _gridSystemVisualSingleArray[gridPositionList[i].x, gridPositionList[i].z].Show();
        }
    }

    private void UpdateGridVisual()
    {
        HideAllGridPositions();
        Unit selectedUnit = UnitActionSystem.Instance.SelectedUnit;
        if (selectedUnit == null) return;
        ShowGridPositionList(selectedUnit.GetMoveAction().GetValidActionGridPositionList());
    }
}