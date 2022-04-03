using ECS.Mono;
using UnityEngine;

namespace ObjectSystems
{
    public class MoveForwardComponent : MonoProvider
    {
        [SerializeField] private float _speed = 0.1f;

        private void Awake()
        {
            var entity = Contexts.game.CreateEntity();
            entity.AddTransform(transform);
            entity.AddMovingForward(_speed);
        }
    }
}