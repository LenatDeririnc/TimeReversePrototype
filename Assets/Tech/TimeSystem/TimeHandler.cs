using System;
using UnityEngine;

namespace TimeSystem
{
    public class TimeHandler
    {
        public const float TickRate = 0.1f;
        public float timeSpeed => _timeSpeed;
        public float scaledTimeSpeed => _timeSpeed * Time.deltaTime;
        public float time => _time;
        public int tickRates => _tickRates;

        public Action OnTickAdd;
        public Action OnTickRemove;

        private int _tickRates = 0;
        private ITimeChanger _onMovementChanged;
        private float _timeSpeed = 0f;
        private float _time = 0f;

        public void SetTimeChanger(ITimeChanger changer)
        {
            _onMovementChanged = changer;
        }

        public void Update()
        {
            UpdateTime();
            UpdateTickRate();
        }

        private void UpdateTickRate()
        {
            var newValue = (int) (_time / TickRate);

            if (newValue > _tickRates)
                OnTickAdd?.Invoke();
            else if (newValue < _tickRates)
                OnTickRemove?.Invoke();

            _tickRates = newValue;
        }

        private void UpdateTime()
        {
            if (_onMovementChanged == null)
                return;

            _timeSpeed = _onMovementChanged.TimeChangerValue();

            if (Input.GetButton("Fire1"))
                _timeSpeed = -1;

            if (_time + scaledTimeSpeed < 0)
            {
                _timeSpeed = 0;
            }

            _time += scaledTimeSpeed;
        }
    }
}