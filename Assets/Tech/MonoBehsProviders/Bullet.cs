﻿using ECS.Mono;
using UnityEngine;

namespace MonoBehsProviders
{
    public class Bullet : MonoGameEntity
    {
        protected override void Awake()
        {
            base.Awake();
            Entity.isBullet = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            Contexts.game.CreateEntity().ReplaceTriggerSignal(Entity, other);
        }
    }
}