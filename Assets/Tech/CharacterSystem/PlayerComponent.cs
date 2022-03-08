using Common;
using ECM.Components;
using ECM.Controllers;
using SingletonSystem;
using TimelineSystem;
using UnityEngine;

namespace CharacterSystem
{
    public class PlayerComponent : Singleton<PlayerComponent>
    {
        public BaseCharacterController CharacterController;
        public CharacterMovement CharacterMovement;
        public MouseLook MouseLook;
        private PlayerTimeline _timeline;

        public IVelocity Velocity { get; private set; }

        public Transform Transform { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Transform = transform;
            Velocity = CharacterMovement;
            _timeline = new PlayerTimeline(this);
        }

        public void SetTransformData(PlayerTimelineInfo data)
        {
            Transform.position = data.position;
            Transform.rotation = data.rotation;
        }

        public PlayerTimelineInfo GetPlayerInfo()
        {
            var info = new PlayerTimelineInfo()
            {
                position = Transform.position,
                rotation = Transform.rotation,
            };

            return info;
        }
    }
}