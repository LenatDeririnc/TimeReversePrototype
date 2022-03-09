using System.Collections.Generic;
using CharacterSystem;
using TimeSystem;

namespace TimelineSystem
{
    public class PlayerTimeline
    {
        private readonly PlayerComponent _playerComponent;
        private Stack<TransformInfo> _timelineData;

        public PlayerTimeline(PlayerComponent playerComponent)
        {
            _timelineData = new Stack<TransformInfo>();
            _playerComponent = playerComponent;
            TimeManagerComponent.TimeManager.OnTickAdd += AddPlayerInfo;
            TimeManagerComponent.TimeManager.OnTickRemove += ExecuteLastPlayerInfo;
        }

        private void AddPlayerInfo()
        {
            var newValue = _playerComponent.GetPlayerInfo();

            _timelineData.Push(newValue);
        }

        private void ExecuteLastPlayerInfo()
        {
            if (_timelineData.Count <= 0)
                return;

            var transformData = _timelineData.Pop();
            _playerComponent.SetTransformData(transformData);
        }
    }
}