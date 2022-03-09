using UnityEngine;

namespace Common
{
    public class VelocityConverter : IVelocity
    {
        private Vector3 _moveVelocity;

        public VelocityConverter() =>
            _moveVelocity = Vector3.zero;

        public VelocityConverter(Vector3 value) =>
            _moveVelocity = value;

        public VelocityConverter(float x, float y, float z) =>
            _moveVelocity = new Vector3(x,y,z);

        public void Set(Vector3 value) => _moveVelocity = value;

        public Vector3 Velocity() =>
            _moveVelocity;
    }
}