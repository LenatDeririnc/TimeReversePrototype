using UnityEngine;

namespace ECM.Components
{
    public interface IPlayerMovementForMouse
    {
        public bool platformUpdatesRotation { get; set; }
        public bool isOnPlatform { get; }
        public Vector3 platformAngularVelocity { get; }
        public Quaternion rotation { get; set; }
    }
}