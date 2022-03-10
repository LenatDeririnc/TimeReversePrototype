using UnityEngine;

namespace Common
{
    public struct TransformInfo
    {
        public Vector3 position;
        public Quaternion rotation;

        public TransformInfo(Transform transform)
        {
            position = transform.position;
            rotation = transform.rotation;
        }
    }
}