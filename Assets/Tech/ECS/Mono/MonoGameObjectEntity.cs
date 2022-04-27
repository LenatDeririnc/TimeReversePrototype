using ECS.Tools;
using UnityEngine;

namespace ECS.Mono
{
    public class MonoGameObjectEntity : MonoProvider
    {
        public GameEntity Entity;

        protected virtual void Awake()
        {
            Entity = GameObjectEntityTools.Instance.FindEntityByGameObject(gameObject);
            if (Entity != null) 
                return;
            
            Entity = Contexts.game.CreateEntity();
            Entity.ReplaceTransform(transform);
            Entity.ReplaceGameObject(gameObject);
            Entity.ReplaceMonoGameObjectEntity(this);
            GameObjectEntityTools.Instance.SetEntityUniqueId(Entity);
            GameObjectEntityTools.Instance.WriteEntityGameObject(Entity);
        }

        public virtual void Destroy()
        {
            Contexts.signals.CreateEntity().ReplaceDestroyEntitySignal(Entity);
        }
    }
}