using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ECM.Components
{
    [RequireComponent(typeof(CapsuleCollider))]
    public abstract class BaseGroundDetection : MonoBehaviour
    {
        #region EDITOR EXPOSED FIELDS
        
        [SerializeField] private LayerMask _groundMask = 1;
        [SerializeField] private float _groundLimit = 60.0f;
        [SerializeField] private float _stepOffset = 0.5f;
        [SerializeField] private float _ledgeOffset;
        [SerializeField] private float _castDistance = 0.5f;
        [SerializeField] private QueryTriggerInteraction _triggerInteraction = QueryTriggerInteraction.Ignore;

        #endregion

        #region FIELDS

        private static readonly Collider[] OverlappedColliders = new Collider[16];

        protected const float kBackstepDistance = 0.05f;
        protected const float kMinCastDistance = 0.01f;
        protected const float kMinLedgeDistance = 0.05f;
        protected const float kMinStepOffset = 0.10f;
        protected const float kHorizontalOffset = 0.001f;

        private CapsuleCollider _capsuleCollider;

        protected GroundHit _groundHitInfo;

        private LayerMask _overlapMask = -1;

        private int _ignoreRaycastLayer = 2;
        private int _cachedLayer;

        #endregion

        #region PROPERTIES

        public LayerMask groundMask
        {
            get => _groundMask;
            set => _groundMask = value;
        }


        public float groundLimit
        {
            get => _groundLimit;
            set => _groundLimit = Mathf.Clamp(value, 0.0f, 89.0f);
        }


        public float stepOffset
        {
            get => _stepOffset;
            set => _stepOffset = Mathf.Clamp(value, kMinStepOffset, capsuleCollider.radius);
        }


        public float ledgeOffset
        {
            get => _ledgeOffset;
            set => _ledgeOffset = Mathf.Clamp(value, 0.0f, capsuleCollider.radius);
        }


        public float castDistance
        {
            get => _castDistance;
            set => _castDistance = Mathf.Max(kMinCastDistance, value);
        }


        public QueryTriggerInteraction triggerInteraction
        {
            get => _triggerInteraction;
            set => _triggerInteraction = value;
        }


        public CapsuleCollider capsuleCollider
        {
            get
            {
                if (_capsuleCollider == null)
                    _capsuleCollider = GetComponent<CapsuleCollider>();

                return _capsuleCollider;
            }
        }


        public bool isOnGround => _groundHitInfo.isOnGround;


        public bool isValidGround => _groundHitInfo.isValidGround;


        public bool isOnLedgeSolidSide => _groundHitInfo.isOnLedgeSolidSide;


        public bool isOnLedgeEmptySide => _groundHitInfo.isOnLedgeEmptySide;


        public float ledgeDistance => _groundHitInfo.ledgeDistance;


        public bool isOnStep => _groundHitInfo.isOnStep;


        public float stepHeight => _groundHitInfo.stepHeight;


        public bool isOnSlope => !Mathf.Approximately(groundAngle, 0.0f);


        public Vector3 groundPoint => _groundHitInfo.groundPoint;


        public Vector3 groundNormal => _groundHitInfo.groundNormal;


        public float groundDistance => _groundHitInfo.groundDistance;


        public Collider groundCollider => _groundHitInfo.groundCollider;


        public Rigidbody groundRigidbody => _groundHitInfo.groundRigidbody;


        public float groundAngle => !isOnGround ? 0.0f : Vector3.Angle(surfaceNormal, transform.up);


        public Vector3 surfaceNormal => _groundHitInfo.surfaceNormal;


        public GroundHit groundHit => _groundHitInfo;


        public GroundHit prevGroundHit { get; private set; }


        public LayerMask overlapMask
        {
            get => _overlapMask;
            set => _overlapMask = value;
        }

        #endregion

        #region METHODS

        protected virtual void InitializeOverlapMask()
        {
            var layer = gameObject.layer;

            _overlapMask = 0;
            for (var i = 0; i < 32; i++)
                if (!Physics.GetIgnoreLayerCollision(layer, i))
                    _overlapMask |= 1 << i;
        }


        public Collider[] OverlapCapsule(Vector3 position, Quaternion rotation, out int overlapCount)
        {
            var center = capsuleCollider.center;
            var radius = capsuleCollider.radius;

            var height = capsuleCollider.height * 0.5f - radius;

            var topSphereCenter = center + Vector3.up * height;
            var bottomSphereCenter = center - Vector3.up * height;

            var top = position + rotation * topSphereCenter;
            var bottom = position + rotation * bottomSphereCenter;

            var colliderCount = Physics.OverlapCapsuleNonAlloc(bottom, top, radius, OverlappedColliders, _overlapMask,
                triggerInteraction);

            overlapCount = colliderCount;
            for (var i = 0; i < colliderCount; i++)
            {
                var overlappedCollider = OverlappedColliders[i];
                if (overlappedCollider != null && overlappedCollider != capsuleCollider)
                    continue;

                if (i < --overlapCount)
                    OverlappedColliders[i] = OverlappedColliders[overlapCount];
            }

            return OverlappedColliders;
        }


        protected bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float distance,
            float backstepDistance = kBackstepDistance)
        {
            origin = origin - direction * backstepDistance;

            var hit = Physics.Raycast(origin, direction, out hitInfo, distance + backstepDistance, groundMask,
                triggerInteraction);
            if (hit)
                hitInfo.distance = hitInfo.distance - backstepDistance;

            return hit;
        }


        protected bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo,
            float distance, float backstepDistance = kBackstepDistance)
        {
            origin = origin - direction * backstepDistance;

            var hit = Physics.SphereCast(origin, radius, direction, out hitInfo, distance + backstepDistance,
                groundMask, triggerInteraction);
            if (hit)
                hitInfo.distance = hitInfo.distance - backstepDistance;

            return hit;
        }


        protected bool CapsuleCast(Vector3 bottom, Vector3 top, float radius, Vector3 direction, out RaycastHit hitInfo,
            float distance, float backstepDistance = kBackstepDistance)
        {
            top = top - direction * backstepDistance;
            bottom = bottom - direction * backstepDistance;

            var hit = Physics.CapsuleCast(bottom, top, radius, direction, out hitInfo, distance + backstepDistance,
                groundMask, triggerInteraction);
            if (hit)
                hitInfo.distance = hitInfo.distance - backstepDistance;

            return hit;
        }


        public virtual bool SweepTest(Vector3 position, Quaternion rotation, Vector3 direction, out RaycastHit hitInfo,
            float distance = Mathf.Infinity, float backstepDistance = kBackstepDistance)
        {
            var radius = capsuleCollider.radius;
            var height = Mathf.Max(0.0f, capsuleCollider.height * 0.5f - radius);

            var bottomSphereCenter = capsuleCollider.center - Vector3.up * height;
            var topSphereCenter = capsuleCollider.center + Vector3.up * height;

            var bottom = position + rotation * bottomSphereCenter;
            var top = position + rotation * topSphereCenter;

            return CapsuleCast(bottom, top, radius, direction, out hitInfo, distance, backstepDistance);
        }


        protected virtual void DisableRaycastCollisions()
        {
            _cachedLayer = gameObject.layer;
            gameObject.layer = _ignoreRaycastLayer;
        }


        protected virtual void EnableRaycastCollisions()
        {
            gameObject.layer = _cachedLayer;
        }


        public virtual void ResetGroundInfo()
        {
            var up = transform.up;

            prevGroundHit = new GroundHit(_groundHitInfo);
            _groundHitInfo = new GroundHit
            {
                groundPoint = transform.position,
                groundNormal = up,
                surfaceNormal = up
            };
        }


        public abstract bool ComputeGroundHit(Vector3 position, Quaternion rotation, ref GroundHit groundHitInfo,
            float distance = Mathf.Infinity);


        public void DetectGround()
        {
            DisableRaycastCollisions();
            ComputeGroundHit(transform.position, transform.rotation, ref _groundHitInfo, castDistance);
            EnableRaycastCollisions();
        }


        public abstract bool FindGround(Vector3 direction, out RaycastHit hitInfo, float distance = Mathf.Infinity,
            float backstepDistance = kBackstepDistance);


        protected virtual void DrawGizmos()
        {
#if UNITY_EDITOR

            if (!Application.isPlaying)
                return;

            if (!isOnGround)
                return;

            var color = new Color(0.0f, 1.0f, 0.0f, 0.25f);
            if (!isValidGround)
                color = new Color(0.0f, 0.0f, 1.0f, 0.25f);

            Handles.color = color;
            Handles.DrawSolidDisc(groundPoint, surfaceNormal, 0.1f);


            Gizmos.color = isValidGround ? Color.green : Color.blue;
            Gizmos.DrawRay(groundPoint, surfaceNormal);


            if (groundNormal != surfaceNormal)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawRay(groundPoint, groundNormal);
            }


            if (!isOnStep)
                return;

            var stepPoint = groundPoint - transform.up * stepHeight;

            Gizmos.color = Color.black;
            Gizmos.DrawLine(groundPoint, stepPoint);

            Handles.color = new Color(0.0f, 0.0f, 0.0f, 0.25f);
            Handles.DrawSolidDisc(stepPoint, transform.up, 0.1f);

#endif
        }

        #endregion

        #region MONOBEHAVIOUR

        protected virtual void OnValidate()
        {
            groundLimit = _groundLimit;
            stepOffset = _stepOffset;
            ledgeOffset = _ledgeOffset;
            castDistance = _castDistance;
        }


        protected virtual void Awake()
        {
            InitializeOverlapMask();

            _ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");
        }


        public void OnDrawGizmosSelected()
        {
            DrawGizmos();
        }

        #endregion
    }
}