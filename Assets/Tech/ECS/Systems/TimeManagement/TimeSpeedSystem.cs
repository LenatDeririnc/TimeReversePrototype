using Entitas;
using UnityEngine;

namespace ECS.Systems.TimeManagement
{
    public class TimeSpeedSystem : IInitializeSystem, IExecuteSystem
    {
        private readonly TimeContext _timeContext;
        private readonly InputContext _inputContext;
        private readonly GameContext _gameContext;

        public TimeSpeedSystem(Contexts contexts)
        {
            _timeContext = contexts.time;
            _inputContext = contexts.input;
            _gameContext = contexts.game;
        }
    
        public void Initialize()
        {
            _timeContext.SetTimeSpeed(0);
        }

        public void Execute()
        {
            if (!_inputContext.hasInputControlling)
                return;
            
            var timeChanger = _inputContext.inputControllingEntity.forwardMovement.Value;
            var timeSpeed = _timeContext.timeSpeed;
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