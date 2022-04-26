using System.Collections.Generic;
using UnityEngine;

namespace ECS.Tools
{
    public class GameObjectEntityTools
    {
        private readonly Contexts _contexts;
        private readonly Dictionary<GameObject, GameEntity> _gameEntityContainer = new Dictionary<GameObject, GameEntity>();
    
        public GameObjectEntityTools(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void WriteEntityGameObject(GameEntity entity)
        {
            _gameEntityContainer[entity.gameObject.Value] = entity;
        }

        public GameEntity FindEntityByGameObject(GameObject obj)
        {
            return _gameEntityContainer.ContainsKey(obj) ? _gameEntityContainer[obj] : null;
        }

        public void SetEntityUniqueId(GameEntity entity)
        {
            if (!_contexts.game.hasMaxEntityID)
                _contexts.game.SetMaxEntityID(0);
        
            _contexts.game.maxEntityID.Value += 1;
            entity.ReplaceEntityID(_contexts.game.maxEntityID.Value);
        }

        public void AddColliderData(Collider collider, GameEntity entity)
        {
            _contexts.game.colliderData.Values[collider] = entity;
        }

        public GameEntity GetEntityByCollider(Collider other)
        {
            return !_contexts.game.colliderData.Values.ContainsKey(other) ? null : _contexts.game.colliderData.Values[other];
        }
    }
}