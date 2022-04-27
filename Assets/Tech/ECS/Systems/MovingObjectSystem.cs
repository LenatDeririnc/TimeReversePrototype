using Entitas;
using UnityEngine;

namespace ECS.Systems
{
    public class MovingObjectSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _group;
        private readonly Contexts _contexts;

        public MovingObjectSystem(Contexts contexts)
        {
            _contexts = contexts;
            _group = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.MovingForward, GameMatcher.Transform));
        }

        public void Execute()
        {
            foreach (var e in _group)
            {
                var transform = e.transform.Value;
                var movingForwardSpeed = e.movingForward.Speed;
                var globalTimeSpeed = _contexts.time.globalTimeSpeed.Value;

                transform.position += transform.forward * movingForwardSpeed * globalTimeSpeed * Time.deltaTime;
            }
        }
    }
}