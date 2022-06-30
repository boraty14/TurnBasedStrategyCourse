using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private ActionButtonUI _actionButtonPrefab;
    [SerializeField] private Transform _actionButtonContainerTransform;
    private BaseAction[] _baseActions;
    
    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += OnSelectedUnitChanged;
        CreateUnitActionButtons();
    }

    private void OnSelectedUnitChanged()
    {
        CreateUnitActionButtons();
    }

    private void CreateUnitActionButtons()
    {
        if(UnitActionSystem.Instance.SelectedUnit == null ) return;

        for (int i = 0; i < _actionButtonContainerTransform.childCount; i++)
        {
            Destroy(_actionButtonContainerTransform.GetChild(i).gameObject);
        }
        
        _baseActions = UnitActionSystem.Instance.SelectedUnit.GetBaseActionArray();
        for (int i = 0; i < _baseActions.Length; i++)
        {
            var actionButtonUI = Instantiate(_actionButtonPrefab, _actionButtonContainerTransform);
            actionButtonUI.SetBaseAction(_baseActions[i]);
            Debug.Log(i);
        }
        
    }
}
