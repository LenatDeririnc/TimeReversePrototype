using UnityEngine;

namespace ECS.Mono
{
    public class MonoGameObjectEntity : MonoProvider
    {
        public GameEntity Entity;

        protected virtual void Awake()
        {
            Entity = EcsManager.GameObjectEntityTools.FindEntityByGameObject(gameObject);
            if (Entity != null) 
                return;
            
            Entity = Contexts.game.CreateEntity();
            Entity.ReplaceTransform(transform);
            Entity.ReplaceGameObject(gameObject);
            Entity.ReplaceMonoGameObjectEntity(this);
            EcsManager.GameObjectEntityTools.SetEntityUniqueId(Entity);
            EcsManager.GameObjectEntityTools.WriteEntityGameObject(Entity);
        }

        public virtual void Destroy()
        {
            Entity.isDestroyEntitySignal = true;
            MonoBehaviour.Destroy(Entity.gameObject.Value);
            // Entity.gameObject.Value.SetActive(false);
        }
    }
}