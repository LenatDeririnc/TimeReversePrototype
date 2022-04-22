using Entitas;
using UnityEngine;

namespace ECS.Systems.TimeManagement
{
    public class TimeSpeedInputSystem : IExecuteSystem, IInitializeSystem
    {
        private readonly TimeContext _timeContext;
        private readonly InputContext _inputContext;
        private readonly GameContext _gameContext;

        public TimeSpeedInputSystem(Contexts contexts)
        {
            _timeContext = contexts.time;
            _inputContext = contexts.input;
            _gameContext = contexts.game;
        }

        public void Initialize()
        {
            _timeContext.SetGlobalTimeSpeed(0);
        }

        public void Execute()
        {
            if (!_inputContext.hasInputControlling)
                return;
            
            var timeChanger = _inputContext.inputControllingEntity.forwardMovement.Value;
            var timeSpeed = _timeContext.globalTimeSpeed;
            var rollbackValue = _timeContext.rollbackValue;
            var isRollback = _timeContext.isRollback;
            
            timeSpeed.Value = timeChanger.magnitude;
            
            if (_gameContext.playerEntity.isDead)
                timeSpeed.Value = Mathf.Clamp(timeSpeed.Value, -1f, 0f);
            
            if (isRollback)
                timeSpeed.Value = -rollbackValue.Value;
        }
    }
}