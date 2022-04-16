using UnityEngine;

namespace CharacterSystem.Player.ECM.Scripts.Controllers
{
    public interface IPlayerMovementForMouse
    {
        public bool platformUpdatesRotation { get; set; }
        public bool isOnPlatform { get; }
        public Vector3 platformAngularVelocity { get; }
        public Quaternion rotation { get; set; }
    }
}