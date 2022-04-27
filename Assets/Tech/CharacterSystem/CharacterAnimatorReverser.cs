using ECS.Mono;
using UnityEngine;

namespace CharacterSystem
{
    public class CharacterAnimatorReverser : MonoGameObjectEntity
    {
        [SerializeField] private Animator _animator;

        protected override void Awake()
        {
            base.Awake();
            Entity.isCharacter = true;
            Entity.ReplaceAnimatorReverser(_animator);
        }
    }
}
