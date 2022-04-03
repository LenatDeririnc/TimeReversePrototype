using UnityEngine;

namespace ECM.Helpers
{
    [RequireComponent(typeof(Animator))]
    public sealed class RootMotionController : MonoBehaviour
    {
        private Animator _animator;
        
        public Vector3 animVelocity { get; private set; }
        public Vector3 animAngularVelocity { get; private set; }

        public Quaternion animDeltaRotation { get; private set; }

        public void Awake()
        {
            _animator = GetComponent<Animator>();

            if (_animator == null)
                Debug.LogError(
                    string.Format(
                        "RootMotionController: There is no 'Animator' attached to the '{0}' game object.\n" +
                        "Please attach a 'Animator' to the '{0}' game object",
                        name));
        }

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