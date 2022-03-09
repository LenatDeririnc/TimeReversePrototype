using Common;
using UnityEngine;

namespace CharacterSystem
{
    public class MoveLookVelocity : IVelocity
    {
        private readonly IVelocity _input;
        private readonly IVelocity _playerMovement;
        private readonly IVelocity _mouseLook;

        public MoveLookVelocity(IVelocity input, IVelocity playerMovement, IVelocity mouseLook)
        {
            _input = input;
            _playerMovement = playerMovement;
            _mouseLook = mouseLook;
        }

        public Vector3 Velocity() =>
            Vector3.ClampMagnitude(_input.Velocity() + _playerMovement.Velocity() + _mouseLook.Velocity() , 1);
    }
}