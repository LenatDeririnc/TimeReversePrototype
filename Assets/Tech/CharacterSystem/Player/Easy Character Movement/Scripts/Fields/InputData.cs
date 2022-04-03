using UnityEngine;

namespace ECM.Fields
{
    public struct InputData
    {
        public Vector2 look;
        public Vector3 direction;
        public bool jump;
        public bool crouch;

        public override string ToString()
        {
            return $"<look: {look}, dir: {direction}, jump: {jump}>";
        }
    }
}