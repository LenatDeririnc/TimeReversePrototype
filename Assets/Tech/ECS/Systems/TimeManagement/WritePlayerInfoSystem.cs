using System.Collections.Generic;
using Common;
using Entitas;

namespace ECS.Systems.TimeManagement
{
    public class WritePlayerInfoSystem : ReactiveSystem<TimeEntity>
    {
        private readonly Contexts _contexts;

        public WritePlayerInfoSystem(Contexts contexts) : base(contexts.time)
        {
            _contexts = contexts;
        }

        protected override ICollector<TimeEntity> GetTrigger(IContext<TimeEntity> context)
        {
            return context.CreateCollector(TimeMatcher.TickRateIncreased.Added());
        }

        protected override bool Filter(TimeEntity entity)
        {
            return true;
        }

        protected override void Execute(List<TimeEntity> entities)
        {
            foreach (var e in entities)
            {
                var playerTransform = _contexts.game.playerEntity.transform.Value;
                var cameraPitch = _contexts.game.playerCameraEntity.cameraPitchAngle.Value;

                var saveData = new TimelineData()
                {
                    playerPosition = playerTransform.position,
                    playerRotation = playerTransform.rotation,
                    cameraAngle = cameraPitch, 
                };
                
                _contexts.time.ReplaceTimelineLastPosition(saveData);
                _contexts.time.timelineDataEntity.timelineData.Value.Push(saveData);
                
                e.isTickRateIncreased = false;
            }
        }
    }
}