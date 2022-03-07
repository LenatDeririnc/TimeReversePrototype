using System;
using System.Collections.Generic;
using CharacterSystem;
using TimeSystem;
using UnityEngine;

namespace TimelineSystem
{
    public class PlayerTimelineComponent : MonoBehaviour
    {
        [SerializeField] private PlayerComponent _playerComponent;
        private PlayerTimeline _timeline;

        private void Awake()
        {
            _timeline = new PlayerTimeline(_playerComponent);
        }
    }

    public class PlayerTimeline
    {
        private readonly PlayerComponent _playerComponent;
        private Stack<PlayerTimelineInfo> _timelineData;

        public PlayerTimeline(PlayerComponent playerComponent)
        {
            _timelineData = new Stack<PlayerTimelineInfo>();
            _playerComponent = playerComponent;
            TimeManager.TimeHandler.OnTickAdd += AddPlayerInfo;
            TimeManager.TimeHandler.OnTickRemove += ExecuteLastPlayerInfo;
        }

        private void AddPlayerInfo()
        {
            var newValue = _playerComponent.GetPlayerInfo();

            _timelineData.Push(newValue);
        }

        private void ExecuteLastPlayerInfo()
        {
            var transformData = _timelineData.Pop();
            _playerComponent.SetTransformData(transformData);
        }
    }

    public struct PlayerTimelineInfo
    {
        public Vector3 position;
        public Quaternion rotation;
    }
}