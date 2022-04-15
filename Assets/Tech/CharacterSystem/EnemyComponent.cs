using ECS;
using UnityEngine;

namespace CharacterSystem
{
    public class EnemyComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private static readonly int Direction = Animator.StringToHash("Direction");

        void Update()
        {
            //TODO: entity
            _animator.SetFloat(Direction, EcsBootstrapper.Contexts.time.timeManagerHandler.Value.timeSpeed);
        }
    }
}
