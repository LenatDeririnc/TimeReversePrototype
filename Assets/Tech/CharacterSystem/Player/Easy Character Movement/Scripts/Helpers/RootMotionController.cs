using System;
using UnityEngine;

namespace ECM.Helpers
{
    public sealed class RootMotionController
    {
        public RootMotionController(Animator animator)
        {
            _animator = animator;
        }

        private Animator _animator;
        
        public Vector3 animVelocity { get; private set; }
        public Vector3 animAngularVelocity { get; private set; }

        public Quaternion animDeltaRotation { get; private set; }

        public void OnAnimatorMove()
        {
            var deltaTime = Time.deltaTime;
            if (deltaTime <= 0.0f)
                return;

            animVelocity = _animator.deltaPosition / deltaTime;

            animDeltaRotation = _animator.deltaRotation;

            float angleInDegrees;
            Vector3 rotationAxis;
            animDeltaRotation.ToAngleAxis(out angleInDegrees, out rotationAxis);

            Vector3 angularDisplacement = rotationAxis * angleInDegrees * Mathf.Deg2Rad;
            animAngularVelocity = angularDisplacement / Time.deltaTime;
        }
    }
}