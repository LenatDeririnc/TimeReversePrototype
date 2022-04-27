using Entitas;
using UnityEngine;

namespace ECS.Systems.Characters.Enemy
{
    public class CharacterAnimationRewindSystem : IExecuteSystem
    {
        private static readonly int Direction = Animator.StringToHash("Direction");
    
        private readonly Contexts _contexts;
        private readonly IGroup<GameEntity> _characters;

        public CharacterAnimationRewindSystem(Contexts contexts)
        {
            _contexts = contexts;
            _characters = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Character, GameMatcher.AnimatorReverser));
        }
    
        public void Execute()
        {
            foreach (var e in _characters)
            {
                e.animatorReverser.Animator.SetFloat(Direction, _contexts.time.globalTimeSpeed.Value);
            }
        }
    }
}