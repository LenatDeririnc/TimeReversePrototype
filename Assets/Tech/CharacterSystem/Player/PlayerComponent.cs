using Common;
using DamageSystem;
using ECM.Components;
using ECM.Controllers;
using SingletonSystem;
using TimelineSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace CharacterSystem.Player
{
    public class PlayerComponent : Singleton<PlayerComponent>, IDestroyable
    {
        [FormerlySerializedAs("CharacterController")] public BasePlayerController playerController;
        [FormerlySerializedAs("CharacterMovement")] public PlayerMovement playerMovement;
        public PlayerRollbackMovement PlayerRollbackMovement;
        public MouseLook MouseLook;
        private PlayerTimeline _timeline;
        public Rigidbody _Rigidbody;

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