using Entitas;
using UnityEngine;

namespace ECS.Systems.Characters.Enemy
{
    public class AnimatorRewindSystem : IExecuteSystem
    {
        private static readonly int Direction = Animator.StringToHash("Direction");
    
        private readonly Contexts _contexts;
        private readonly IGroup<GameEntity> _animatorGroup;

        public AnimatorRewindSystem(Contexts contexts)
        {
            _contexts = contexts;
            _animatorGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.AnimatorReverser));
        }
    
        public void Execute()
        {
            foreach (var e in _animatorGroup)
            {
                e.animatorReverser.Animator.SetFloat(Direction, _contexts.time.globalTimeSpeed.Value);
            }
        }
    }
}