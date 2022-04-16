﻿using ECS.Extensions;
using Entitas;

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
                var transform = e.transform;
                var movingForward = e.movingForward;

                transform.Value.position += 
                    transform.Value.forward * 
                    movingForward.Speed * 
                    _contexts.time.ScaledTimeSpeed();
            }
        }
    }
}