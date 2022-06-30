using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Actions
{
    public abstract class BaseAction : MonoBehaviour
    {
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
    }
}
