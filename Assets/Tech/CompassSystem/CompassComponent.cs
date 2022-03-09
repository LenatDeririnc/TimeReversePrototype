using System;
using CharacterSystem;
using Common;
using Helpers;
using InputHandler;
using SingletonSystem;
using TimeSystem;
using UnityEngine;

namespace CompassSystem
{
    public class CompassComponent : Singleton<CompassComponent>
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Sector _rollbackSector;
        public Transform Transform { get; private set; }
        private IVelocity _updatePositionVelocity;

        public Sector RollbackSector => _rollbackSector;
        public IVelocity Velocity => _updatePositionVelocity;


        protected override void Awake()
        {
            base.Awake();
            Transform = transform;
            _rollbackSector.transform = Transform;
        }

        private void Start()
        {
            UpdateForward();
            SetVelocitySetter(new MoveLookVelocity(
                InputHandlerComponent.Instance.moveDirectionVelocity,
                PlayerComponent.Instance.CharacterMovement,
                PlayerComponent.Instance.MouseLook));
        }

        public void SetVelocitySetter(IVelocity component)
        {
            _updatePositionVelocity = component ?? throw new InvalidCastException();
        }

        public void UpdateForward()
        {
            Transform.position = _playerTransform.position;
            transform.rotation = _playerTransform.rotation;
        }

        private void Update()
        {
            UpdateForward();
        }
    }


}