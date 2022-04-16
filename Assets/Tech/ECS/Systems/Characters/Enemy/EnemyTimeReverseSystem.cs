using Entitas;
using UnityEngine;

namespace ECS.Systems.Characters.Enemy
{
    public class EnemyTimeReverseSystem : IExecuteSystem
    {
        private static readonly int Direction = Animator.StringToHash("Direction");
    
        private readonly Contexts _contexts;
        private readonly IGroup<GameEntity> _enemiesFilter;

        public EnemyTimeReverseSystem(Contexts contexts)
        {
            _contexts = contexts;
            _enemiesFilter = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Enemy, GameMatcher.AnimatorReverser));
        }
    
        public void Execute()
        {
            foreach (var e in _enemiesFilter)
            {
                e.animatorReverser.Animator.SetFloat(Direction, _contexts.time.timeSpeed.Value);
            }
        }
    }
}