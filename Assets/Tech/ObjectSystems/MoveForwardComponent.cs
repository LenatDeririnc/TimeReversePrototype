using System;
using TimeSystem;
using UnityEngine;

namespace ObjectSystems
{
    public class MoveForwardComponent : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.1f;

        private void Awake()
        {
            var context = Contexts.sharedInstance;
            var entity = context.game.CreateEntity();
            entity.AddTransform(transform);
            entity.AddMovingForward(_speed);
        }
    }
}