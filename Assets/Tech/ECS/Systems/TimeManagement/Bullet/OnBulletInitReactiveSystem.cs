using System.Collections.Generic;
using Entitas;
using TimelineData;

namespace ECS.Systems.TimeManagement.Bullet
{
    public class OnBulletInitReactiveSystem : ReactiveSystem<GameEntity>, IInitializeSystem
    {
        private readonly Contexts _contexts;
        private readonly IGroup<GameEntity> _bullets;

        public OnBulletInitReactiveSystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
            _bullets = contexts.game.GetGroup(GameMatcher.Bullet);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Bullet.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var time = _contexts.time.time.Value;
            var timelineStack = _contexts.time.timeLineStack.Value;
        
            foreach (var bullet in entities)
            {
                var saveData = new BulletTimelineData(bullet.entityID.Value, time)
                {
                    Enabled = true,
                };
                timelineStack.Push(saveData);
            }
        }

        public void Initialize()
        {
            var timelineStack = _contexts.time.timeLineStack.Value;
        
            foreach (var bullet in _bullets)
            {
                var saveData = new BulletTimelineData(bullet.entityID.Value, -100)
                {
                    Enabled = true,
                };
                timelineStack.Push(saveData);
            }
        }
    }
}