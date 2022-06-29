using System;
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
                _isActive = false;
                _onActionComplete?.Invoke();
            }
        }

        public void Spin(Action clearBusy)
        {
            _totalSpinAmount = 0f;
            _isActive = true;
            _onActionComplete = clearBusy;
        }
    }
}
