using System;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    public Action OnSelectedUnitChanged;
    public Unit SelectedUnit => _selectedUnit;

    [SerializeField] private LayerMask _unitLayerMask;
    private Unit _selectedUnit;
    private bool _isBusy = false;

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

    private void Update()
    {
        if (_isBusy) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;
            if (_selectedUnit == null) return;

            var mouseWorldPosition = MouseWorld.GetPosition();
            var mouseGridPosition = LevelGrid.Instance.GetGridPosition(mouseWorldPosition);
            if (!_selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition)) return;

            _selectedUnit.GetMoveAction().Move(mouseGridPosition,ClearBusy);
            SetBusy();
        }

        else if (Input.GetMouseButtonDown(1))
        {
            if (TryHandleUnitSelection()) return;
            if (_selectedUnit == null) return;
            _selectedUnit.GetSpinAction().Spin(ClearBusy);
            SetBusy();
        }
    }

    private void SetBusy() => _isBusy = true;
    private void ClearBusy() => _isBusy = false;

    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _unitLayerMask)) return false;

        if (!hit.transform.TryGetComponent<Unit>(out _selectedUnit)) return false;
        SetSelectedUnit();
        return true;
    }

    private void SetSelectedUnit()
    {
        OnSelectedUnitChanged?.Invoke();
    }
}