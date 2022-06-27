using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10 )]
public class UnitActionSystem : MonoBehaviour
{
    [SerializeField] private LayerMask _unitLayerMask;
    
    private Unit _selectedUnit;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        if(TryHandleUnitSelection()) return;
        if (_selectedUnit == null) return;
        _selectedUnit.Move(MouseWorld.GetPosition());
    }

    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _unitLayerMask)) return false;

        return hit.transform.TryGetComponent<Unit>(out _selectedUnit);

    }
}
