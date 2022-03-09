using System;
using Common;
using CompassSystem;
using Helpers;
using InputHandler;
using SingletonSystem;
using TMPro;
using UnityEngine;

namespace TimeSystem
{
    public class RollbackController : Singleton<RollbackController>
    {
        public CompassComponent CompassComponent;

        private void Start()
        {
            CompassComponent = CompassComponent.Instance;
        }

        public bool IsRollbackActive()
            => InputHandlerComponent.Instance.BackMovement.Velocity().magnitude > 0;

        public bool IsRollbackAngle() =>
            Sector.Intersection(
                CompassComponent.Transform.TransformDirection(InputHandlerComponent.Instance.BackMovement.Velocity()),
                CompassComponent.RollbackSector);
    }
}