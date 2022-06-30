using System;
using Actions;
using UnityEngine;
using UnityEngine.EventSystems;

[DefaultExecutionOrder(-10)]
public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    public Action OnSelectedUnitChanged;
    public Unit SelectedUnit => _selectedUnit;

    [SerializeField] private LayerMask _unitLayerMask;
    private Unit _selectedUnit;
    private bool _isBusy = false;
    private BaseAction _selectedAction;

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
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (TryHandleUnitSelection()) return;
        if (_selectedUnit == null) return;

        HandleSelectedAction();
    }

    private void HandleSelectedAction()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        var mouseWorldPosition = MouseWorld.GetPosition();
        var mouseGridPosition = LevelGrid.Instance.GetGridPosition(mouseWorldPosition);

        if (!_selectedAction.IsValidActionGridPosition(mouseGridPosition)) return;

        SetBusy();
        _selectedAction.TakeAction(mouseGridPosition, ClearBusy);
    }

    private void SetBusy() => _isBusy = true;
    private void ClearBusy() => _isBusy = false;

    private bool TryHandleUnitSelection()
    {
        if (!Input.GetMouseButtonDown(0)) return false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _unitLayerMask)) return false;

        if (hit.transform.TryGetComponent<Unit>(out Unit unit))
        {
            if (unit == _selectedUnit) return false;
            SetSelectedUnit(unit);
            return true;
        }

        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        _selectedUnit = unit;
        SetSelectedAction(_selectedUnit.GetMoveAction());
        OnSelectedUnitChanged?.Invoke();
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        _selectedAction = baseAction;
    }

    public BaseAction GetSelectedAction() => _selectedAction;
}