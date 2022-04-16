using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace ECS.Systems
{
    public class ColliderDataSystem : IInitializeSystem
    {
        private readonly Contexts _contexts;

        public ColliderDataSystem(Contexts contexts)
        {
            _contexts = contexts;
        }
    
        public void Initialize()
        {
            _contexts.game.SetColliderData(new Dictionary<Collider, GameEntity>());
        }
    }
}