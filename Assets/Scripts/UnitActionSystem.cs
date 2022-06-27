using System;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class UnitActionSystem : MonoBehaviour
{
    [SerializeField] private LayerMask _unitLayerMask;
    
    public Action OnSelectedUnitChanged;
    public Unit SelectedUnit => _selectedUnit;

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

        if (!hit.transform.TryGetComponent<Unit>(out _selectedUnit)) return false;
        SetSelectedUnit();
        return true;

    }

    private void SetSelectedUnit()
    {
        OnSelectedUnitChanged?.Invoke();
    }

}
