using System;
using Entitas;
using TimelineData;

namespace ECS.Systems.TimeManagement.Bullet
{
    public class UndoBulletTimeLineSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;
        private readonly IGroup<GameEntity> _bullets;
        
        public UndoBulletTimeLineSystem(Contexts contexts)
        {
            _contexts = contexts;
            _bullets = _contexts.game.GetGroup(GameMatcher.Bullet);
        }
    
        public void Execute()
        {
            if (!_contexts.time.timeEntity.isTickRateDecreased)
                return;
                
            var time = _contexts.time.time.Value;
            var timelineStack = _contexts.time.timeLineStack.Value;
        
            foreach (var bullet in _bullets)
            {
                if (timelineStack.Pop(bullet.entityID.Value, time) is BulletTimelineData element) 
                    continue;
                
                bullet.monoGameObjectEntity.Value.Destroy();
            }
        }
    }
}