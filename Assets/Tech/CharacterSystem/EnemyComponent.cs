using ECS.Mono;
using UnityEngine;

namespace CharacterSystem
{
    public class EnemyComponent : MonoProvider
    {
        [SerializeField] private Animator _animator;

        private void Start()
        {
            var entity = Contexts.game.CreateEntity();
            entity.isEnemy = true;
            entity.ReplaceAnimatorReverser(_animator);
        }
    }
}
