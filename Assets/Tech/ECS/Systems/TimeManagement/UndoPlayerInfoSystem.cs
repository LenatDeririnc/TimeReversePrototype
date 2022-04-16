using System.Collections.Generic;
using Common;
using Entitas;

namespace ECS.Systems.TimeManagement
{
    public class UndoPlayerInfoSystem : ReactiveSystem<TimeEntity>
    {
        private readonly Contexts _contexts;

        public UndoPlayerInfoSystem(Contexts contexts) : base(contexts.time)
        {
            _contexts = contexts;
        }
        
        protected override ICollector<TimeEntity> GetTrigger(IContext<TimeEntity> context)
        {
            return context.CreateCollector(TimeMatcher.TickRateDecreased.Added());
        }
        
        protected override bool Filter(TimeEntity entity)
        {
            return true;
        }

        protected override void Execute(List<TimeEntity> entities)
        {
            foreach (var e in entities)
            {
                var timelineData = _contexts.time.timelineDataEntity.timelineData.Value;
            
                if (timelineData.Count <= 0)
                    return;
                
                var player = _contexts.game.playerEntity;
                var camera = _contexts.game.playerCameraEntity;
                
                var newTimelineData = new TimelineData()
                {
                    playerPosition = player.transform.Value.position,
                    playerRotation = player.transform.Value.rotation,
                    cameraAngle = camera.cameraPitchAngle.Value,
                };
                
                _contexts.time.ReplaceTimelineLastPosition(newTimelineData);
                TimelineData transformData = timelineData.Pop();
                _contexts.time.ReplaceTimelineRewindPosition(transformData);
                
                e.isTickRateDecreased = false;
            }
        }
    }
}