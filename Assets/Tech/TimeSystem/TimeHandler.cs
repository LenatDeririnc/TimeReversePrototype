using UnityEngine;

namespace TimeSystem
{
    public class TimeHandler
    {
        private readonly ITimeChanger _onMovementChanged;
        private float currentTime = 0f;

        public TimeHandler(ITimeChanger onMovementChanged)
        {
            _onMovementChanged = onMovementChanged;
        }

        public void UpdateTime()
        {
            currentTime = _onMovementChanged.TimeChangerValue();
            Time.timeScale = currentTime;
        }
    }
}