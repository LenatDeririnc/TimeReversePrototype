using ECS.Mono;
using UnityEngine;

namespace MonoBehsProviders
{
    public class MoveForwardComponent : MonoGameObjectEntity
    {
        [SerializeField] private float _speed = 0.1f;

        protected override void Awake()
        {
            base.Awake();
            Entity.ReplaceMovingForward(_speed);
        }
    }
}