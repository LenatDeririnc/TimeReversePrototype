using Common;
using DamageSystem;
using ECM;
using ECM.Components;
using ECM.Controllers;
using SingletonSystem;
using TimelineSystem;
using UnityEngine;

namespace CharacterSystem.Player
{
    public class PlayerComponent : Singleton<PlayerComponent>, IDestroyable
    {
        public PlayerModel Model;
        public PlayerController Controller; 
        public PlayerRollbackMovement PlayerRollbackMovement;
        private PlayerTimeline _timeline;

        public Transform Transform { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Transform = transform;
            _timeline = new PlayerTimeline(this);
            PlayerRollbackMovement = new PlayerRollbackMovement(Transform);
        }

        private void Update()
        {
            PlayerRollbackMovement.Update();
        }

        public TransformInfo GetPlayerTransformInfo()
        {
            var info = new TransformInfo()
            {
                position = Transform.position,
                rotation = Transform.rotation,
            };

            return info;
        }

        public void Destroy()
        {
            Debug.Log("YOU DEAD");
        }
    }
}