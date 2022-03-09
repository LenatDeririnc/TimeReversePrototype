using System;
using DamageSystem;
using ECM.Components;
using ECM.Controllers;
using SingletonSystem;
using TimelineSystem;
using TimeSystem;
using UnityEngine;

namespace CharacterSystem
{
    public class PlayerComponent : Singleton<PlayerComponent>, IDestroyable
    {
        public BaseCharacterController CharacterController;
        public CharacterMovement CharacterMovement;
        public MouseLook MouseLook;
        private PlayerTimeline _timeline;
        public Rigidbody _Rigidbody;

        public Transform Transform { get; private set; }

        public TransformInfo newData;
        public TransformInfo lastPosition;

        private bool isPositionUpdated = false;

        protected override void Awake()
        {
            base.Awake();
            Transform = transform;
            _timeline = new PlayerTimeline(this);
        }

        private void Update()
        {
            if (!RollbackController.Instance.IsRollbackActive())
            {
                isPositionUpdated = false;
                return;
            }

            //TODO: Переделать
            if (!isPositionUpdated)
                return;

            var tm = TimeManagerComponent.TimeManager;
            Transform.position = Vector3.Lerp(lastPosition.position, newData.position, 1-tm.TickRateDivideRatio);
            Transform.rotation = Quaternion.Lerp(lastPosition.rotation, newData.rotation, 1-tm.TickRateDivideRatio);
        }

        public void SetTransformData(TransformInfo data)
        {
            newData = data;
            lastPosition = new TransformInfo()
            {
                position = Transform.position,
                rotation = Transform.rotation,
            };
            isPositionUpdated = true;
        }

        public TransformInfo GetPlayerInfo()
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