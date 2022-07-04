using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Actions
{
    public class SpinAction : BaseAction
    {
        [SerializeField] private float _spinSpeed;
        private float _totalSpinAmount = 0f;
        
        private void Update()
        {
            if(!_isActive) return;

            var spinAmount = _spinSpeed * Time.deltaTime;
            transform.eulerAngles += spinAmount * Vector3.up;
            _totalSpinAmount += spinAmount;
            if (_totalSpinAmount > 360)
            {
                _totalSpinAmount = 0;
                ActionComplete();
            }
        }

        public override void TakeAction(GridPosition gridPosition, Action clearBusy)
        {
            _totalSpinAmount = 0f;
            ActionStart(clearBusy);
        }


        public override List<GridPosition> GetValidActionGridPositionList()
        {
            _validGridPositionList.Clear();
            _validGridPositionList.Add(_unit.GetGridPosition());
            return _validGridPositionList;
        }
        
        public override string GetActionName() => "Spin";
    }
}
