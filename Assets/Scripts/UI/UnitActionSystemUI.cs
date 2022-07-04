using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using TMPro;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private ActionButtonUI _actionButtonPrefab;
    [SerializeField] private Transform _actionButtonContainerTransform;
    [SerializeField] private TextMeshProUGUI _actionPointsText;
    private BaseAction[] _baseActions;

    private List<ActionButtonUI> _actionButtonUIList;

    private void Awake()
    {
        _actionButtonUIList = new List<ActionButtonUI>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += OnSelectedActionChanged;
        UnitActionSystem.Instance.OnActionStarted += OnActionStarted;
        Unit.OnAnyActionPointsChanged += OnAnyActionPointsChanged;
        CreateUnitActionButtons();
    }

    private void OnAnyActionPointsChanged()
    {
        UpdateActionPoints();
    }

    private void OnActionStarted()
    {
        UpdateActionPoints();
    }

    private void OnSelectedActionChanged()
    {
        UpdateSelectedVisual();
    }

    private void OnSelectedUnitChanged()
    {
        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoints();
    }

    private void CreateUnitActionButtons()
    {
        if(UnitActionSystem.Instance.SelectedUnit == null ) return;

        for (int i = 0; i < _actionButtonContainerTransform.childCount; i++)
        {
            Destroy(_actionButtonContainerTransform.GetChild(i).gameObject);
        }
        _actionButtonUIList.Clear();
        
        _baseActions = UnitActionSystem.Instance.SelectedUnit.GetBaseActionArray();
        for (int i = 0; i < _baseActions.Length; i++)
        {
            var actionButtonUI = Instantiate(_actionButtonPrefab, _actionButtonContainerTransform);
            actionButtonUI.SetBaseAction(_baseActions[i]);
            _actionButtonUIList.Add(actionButtonUI);
        }
        UpdateSelectedVisual();
    }

    private void UpdateSelectedVisual()
    {
        for (int i = 0; i < _actionButtonUIList.Count; i++)
        {
            _actionButtonUIList[i].UpdateSelectedVisual();
        }
    }

    private void UpdateActionPoints()
    {
        Unit selectedUnit = UnitActionSystem.Instance.SelectedUnit;
        if (selectedUnit == null) return;
        _actionPointsText.text = "Action Points: " + selectedUnit.GetActionPoints();
    }
}
