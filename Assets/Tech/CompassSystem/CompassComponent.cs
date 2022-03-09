using System;
using CharacterSystem;
using Common;
using Helpers;
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
        private IVelocity _velocity;

        public Sector RollbackSector => _rollbackSector;
        public IVelocity Velocity => _velocity;


        protected override void Awake()
        {
            base.Awake();
            Transform = transform;
            _rollbackSector.transform = Transform;
            TimeManagerComponent.TimeManager.SetCompassObject(this);
        }

        private void Start()
        {
            UpdateForward();
            SetVelocitySetter(new PlayerMovementVelocity(PlayerComponent.Instance.CharacterMovement, PlayerComponent.Instance.MouseLook));
        }

        public void SetVelocitySetter(IVelocity component)
        {
            _velocity = component ?? throw new InvalidCastException();
        }

        public bool IsMoving()
        {
            return _velocity.Velocity().magnitude > 0;
        }

        public bool IsRollbackAngle()
        {
            return Sector.Intersection(_velocity.Velocity(), _rollbackSector);
        }

        public void UpdateForward()
        {
            Transform.position = _playerTransform.position;
            transform.rotation = _playerTransform.rotation;
        }

        private void Update()
        {
            if (!IsMoving())
                return;

            UpdateForward();
        }
    }


}