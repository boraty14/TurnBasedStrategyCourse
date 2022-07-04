using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Actions
{
    public abstract class BaseAction : MonoBehaviour
    {
        [SerializeField] private int _actionCost = 1;
        protected Unit _unit;
        protected bool _isActive = false;
        protected Action _onActionComplete;
        protected List<GridPosition> _validGridPositionList = new List<GridPosition>();
        
        protected virtual void Awake()
        {
            _unit = GetComponent<Unit>();
        }

        public abstract string GetActionName();
        public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

        public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
        {
            var validActionGridPosition = GetValidActionGridPositionList();
            return validActionGridPosition.Contains(gridPosition);
        }

        public abstract List<GridPosition> GetValidActionGridPositionList();
        public int GetActionPointsCost() => _actionCost;

        protected void ActionStart(Action onActionComplete)
        {
            _isActive = true;
            _onActionComplete = onActionComplete;
        }

        protected void ActionComplete()
        {
            _isActive = false;
            _onActionComplete?.Invoke();
        }
    }
}
