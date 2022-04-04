using System;
using System.Collections.Generic;
using Common;
using Entitas;
using UnityEngine;

namespace ECS.Systems.TimeManagement
{
    public class TimelineSystem : IInitializeSystem
    {
        private readonly Contexts _contexts;

        public TimelineSystem(Contexts contexts)
        {
            _contexts = contexts;
        }
        
        public void Initialize()
        {
            _contexts.time.SetTimelineData(new Stack<TransformInfo>());
            _contexts.time.SetTimelineLastPosition(_contexts.game.playerEntity.transformInfo.Value);

            var timeManager = _contexts.time.timeManagerHandlerEntity.timeManagerHandler;

            if (timeManager == null)
                throw new Exception("Надо поместить TimelineSystem после инициализации timeManagerHandler");
            
            timeManager.Value.OnTickAdd += AddPlayerInfo;
            timeManager.Value.OnTickRemove += ExecuteLastPlayerInfo;
        }

        private void AddPlayerInfo()
        {
            var data = new TransformInfo(_contexts.game.playerEntity.transform.Value);
            _contexts.time.timelineDataEntity.timelineData.Value.Push(data);
            _contexts.time.ReplaceTimelineLastPosition(data);
            Debug.Log($"Add: {data.ToString()}");
        }

        private void ExecuteLastPlayerInfo()
        {
            var timelineData = _contexts.time.timelineDataEntity.timelineData.Value;
            
            if (timelineData.Count <= 0)
                return;
            
            TransformInfo transformData = timelineData.Pop();
            Debug.Log($"Execute: {transformData.ToString()}");
            _contexts.game.playerEntity.ReplaceTransformInfo(transformData);
            _contexts.time.ReplaceTimelineLastPosition(transformData);
        }
    }
}