using Common;
using UnityEngine;

namespace CompassSystem
{
    public class PlayerMovementVelocity : IVelocity
    {
        private readonly IVelocity _playerMovement;
        private readonly IVelocity _mouseLook;

        public PlayerMovementVelocity(IVelocity playerMovement, IVelocity mouseLook)
        {
            _playerMovement = playerMovement;
            _mouseLook = mouseLook;
        }

        public Vector3 Velocity() =>
            Vector3.ClampMagnitude(_playerMovement.Velocity() + _mouseLook.Velocity() , 1);
    }
}