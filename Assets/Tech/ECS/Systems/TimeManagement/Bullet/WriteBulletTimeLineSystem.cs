using DesperateDevs.Utils;
using Entitas;
using TimelineData;

namespace ECS.Systems.TimeManagement.Bullet
{
    public class WriteBulletTimeLineSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;
        private readonly IGroup<GameEntity> _bullets;

        public WriteBulletTimeLineSystem(Contexts contexts)
        {
            _contexts = contexts;
            _bullets = _contexts.game.GetGroup(GameMatcher.Bullet);
        }
    
        public void Execute()
        {
            if (!_contexts.time.timeEntity.isTickRateIncreased)
                return;
        
            var time = _contexts.time.time.Value;
            var timelineStack = _contexts.time.timeLineStack.Value;
        
            foreach (var bullet in _bullets)
            {
                var saveData = new BulletTimelineData(bullet.entityID.Value, time)
                {
                    Enabled = bullet.gameObject.Value.activeSelf,
                };
                timelineStack.Push(saveData);
            }
        }
    }
}