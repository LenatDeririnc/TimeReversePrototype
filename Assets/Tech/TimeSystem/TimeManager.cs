using System;
using Common;
using UnityEngine;

namespace TimeSystem
{
    public class TimeManager
    {
        public const float TickRate = 0.1f;

        public float TickRateCount => (_time / TickRate);
        public float TickRateMod => (_time % TickRate);
        public float TickRateDivideRatio => (TickRateMod / TickRate);
        public float timeSpeed => _timeSpeed;
        public float time => _time;
        public int tickRates => _tickRates;
        private IVelocity _velocityCharacter;

        public Action OnTickAdd;
        public Action OnTickRemove;

        private int _tickRates = 0;
        private float _timeSpeed = 0f;
        private float _time = 0f;

        public void SetMovingObject(IVelocity changer)
        {
            _velocityCharacter = changer;
        }

        public void Update()
        {
            UpdateTimeSpeed();
            UpdateTime();
            UpdateTickRate();
        }

        private void UpdateTickRate()
        {
            int newValue = (int) TickRateCount;

            if (newValue > _tickRates)
                OnTickAdd?.Invoke();
            else if (newValue < _tickRates)
                OnTickRemove?.Invoke();

            _tickRates = newValue;
        }

        private float ScaledTimeSpeed()
        {
            return _timeSpeed * Time.deltaTime;
        }

        private bool IsRollback()
        {
            return RollbackController.Instance.IsRollbackActive() && RollbackController.Instance.IsRollbackAngle();
        }

        private void UpdateTimeSpeed()
        {
            if (_velocityCharacter == null)
                return;

            _timeSpeed = _velocityCharacter.Velocity().magnitude;

            if (IsRollback())
                _timeSpeed = -1;
        }

        private void UpdateTime()
        {
            if (_time + ScaledTimeSpeed() < 0)
                _timeSpeed = 0;

            _time += ScaledTimeSpeed();
        }
    }
}