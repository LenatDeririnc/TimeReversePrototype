using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ECM.Components
{
    public sealed class GroundDetection : BaseGroundDetection
    {
        #region METHODS

        private bool BottomSphereCast(Vector3 position, Quaternion rotation, out RaycastHit hitInfo, float distance,
            float backstepDistance = kBackstepDistance)
        {
            var radius = capsuleCollider.radius;

            var height = Mathf.Max(0.0f, capsuleCollider.height * 0.5f - radius);
            var center = capsuleCollider.center - Vector3.up * height;

            var origin = position + rotation * center;
            var down = rotation * Vector3.down;

            return SphereCast(origin, radius, down, out hitInfo, distance, backstepDistance);
        }


        private bool BottomRaycast(Vector3 position, Quaternion rotation, out RaycastHit hitInfo, float distance,
            float backstepDistance = kBackstepDistance)
        {
            var down = rotation * Vector3.down;
            return Raycast(position, down, out hitInfo, distance, backstepDistance) &&
                   SimulateSphereCast(position, rotation, hitInfo.normal, out hitInfo, distance, backstepDistance);
        }


        private bool SimulateSphereCast(Vector3 position, Quaternion rotation, Vector3 normal, out RaycastHit hitInfo,
            float distance = Mathf.Infinity, float backstepDistance = kBackstepDistance)
        {
            var origin = position;
            var up = rotation * Vector3.up;

            var angle = Vector3.Angle(normal, up) * Mathf.Deg2Rad;
            if (angle > 0.0001f)
            {
                var radius = capsuleCollider.radius;

                var x = Mathf.Sin(angle) * radius;
                var y = (1.0f - Mathf.Cos(angle)) * radius;

                var right = Vector3.Cross(normal, up);
                var tangent = Vector3.Cross(right, normal);

                origin += Vector3.ProjectOnPlane(tangent, up).normalized * x + up * y;
            }

            return Raycast(origin, -up, out hitInfo, distance, backstepDistance);
        }


        private void DetectLedgeAndSteps(Vector3 position, Quaternion rotation, ref GroundHit groundHitInfo,
            float distance, Vector3 point, Vector3 normal)
        {
            Vector3 up = rotation * Vector3.up, down = -up;
            var projectedNormal = Vector3.ProjectOnPlane(normal, up).normalized;

            var nearPoint = point + projectedNormal * kHorizontalOffset;
            var farPoint = point - projectedNormal * kHorizontalOffset;

            var ledgeStepDistance = Mathf.Max(kMinLedgeDistance, Mathf.Max(stepOffset, distance));

            RaycastHit nearHitInfo;
            var nearHit = Raycast(nearPoint, down, out nearHitInfo, ledgeStepDistance);
            var isNearGroundValid = nearHit && Vector3.Angle(nearHitInfo.normal, up) < groundLimit;

            RaycastHit farHitInfo;
            var farHit = Raycast(farPoint, down, out farHitInfo, ledgeStepDistance);
            var isFarGroundValid = farHit && Vector3.Angle(farHitInfo.normal, up) < groundLimit;


            if (farHit && !isFarGroundValid)
            {
                groundHitInfo.surfaceNormal = farHitInfo.normal;


                RaycastHit secondaryHitInfo;
                if (BottomRaycast(position, rotation, out secondaryHitInfo, distance))
                {
                    groundHitInfo.SetFrom(secondaryHitInfo);
                    groundHitInfo.surfaceNormal = secondaryHitInfo.normal;
                }

                return;
            }


            if (isNearGroundValid && isFarGroundValid)
            {
                groundHitInfo.surfaceNormal =
                    (point - nearHitInfo.point).sqrMagnitude < (point - farHitInfo.point).sqrMagnitude
                        ? nearHitInfo.normal
                        : farHitInfo.normal;


                var nearHeight = Vector3.Dot(point - nearHitInfo.point, up);
                var farHeight = Vector3.Dot(point - farHitInfo.point, up);

                var height = Mathf.Max(nearHeight, farHeight);
                if (height > kMinLedgeDistance && height < stepOffset)
                {
                    groundHitInfo.isOnStep = true;
                    groundHitInfo.stepHeight = height;
                }

                return;
            }


            var isOnLedge = isNearGroundValid != isFarGroundValid;
            if (!isOnLedge)
                return;


            groundHitInfo.surfaceNormal = isFarGroundValid ? farHitInfo.normal : nearHitInfo.normal;

            groundHitInfo.ledgeDistance = Vector3.ProjectOnPlane(point - position, up).magnitude;

            if (isFarGroundValid && groundHitInfo.ledgeDistance > ledgeOffset)
            {
                groundHitInfo.isOnLedgeEmptySide = true;

                var radius = ledgeOffset;
                var offset = Mathf.Max(0.0f, capsuleCollider.height * 0.5f - radius);

                var bottomSphereCenter = capsuleCollider.center - Vector3.up * offset;
                var bottomSphereOrigin = position + rotation * bottomSphereCenter;

                RaycastHit hitInfo;
                if (SphereCast(bottomSphereOrigin, radius, down, out hitInfo, Mathf.Max(stepOffset, distance)))
                {
                    var verticalSquareDistance = Vector3.Project(point - hitInfo.point, up).sqrMagnitude;
                    if (verticalSquareDistance <= stepOffset * stepOffset)
                        groundHitInfo.isOnLedgeEmptySide = false;
                }
            }

            groundHitInfo.isOnLedgeSolidSide = !groundHitInfo.isOnLedgeEmptySide;
        }

        public override bool ComputeGroundHit(Vector3 position, Quaternion rotation, ref GroundHit groundHitInfo,
            float distance = Mathf.Infinity)
        {
            var up = rotation * Vector3.up;


            RaycastHit hitInfo;
            if (BottomSphereCast(position, rotation, out hitInfo, distance) &&
                Vector3.Angle(hitInfo.normal, up) < 89.0f)
            {
                groundHitInfo.SetFrom(hitInfo);


                DetectLedgeAndSteps(position, rotation, ref groundHitInfo, distance, hitInfo.point, hitInfo.normal);


                groundHitInfo.isOnGround = true;
                groundHitInfo.isValidGround = !groundHitInfo.isOnLedgeEmptySide &&
                                              Vector3.Angle(groundHitInfo.surfaceNormal, up) < groundLimit;

                return true;
            }


            if (!BottomRaycast(position, rotation, out hitInfo, distance))
                return false;


            groundHitInfo.SetFrom(hitInfo);
            groundHitInfo.surfaceNormal = hitInfo.normal;

            groundHitInfo.isOnGround = true;
            groundHitInfo.isValidGround = Vector3.Angle(groundHitInfo.surfaceNormal, /*Vector3.up*/up) < groundLimit;

            return true;
        }

        public override bool FindGround(Vector3 direction, out RaycastHit hitInfo, float distance = Mathf.Infinity,
            float backstepDistance = kBackstepDistance)
        {
            var radius = capsuleCollider.radius;

            var height = Mathf.Max(0.0f, capsuleCollider.height * 0.5f - radius);
            var center = capsuleCollider.center - Vector3.up * height;

            var origin = transform.TransformPoint(center);

            var up = transform.up;
            if (!SphereCast(origin, radius, direction, out hitInfo, distance, backstepDistance) ||
                Vector3.Angle(hitInfo.normal, /*Vector3.up*/up) >= 89.0f)
                return false;

            var p = transform.position - transform.up * hitInfo.distance;
            var q = transform.rotation;

            var groundHitInfo = new GroundHit(hitInfo);
            DetectLedgeAndSteps(p, q, ref groundHitInfo, castDistance, hitInfo.point, hitInfo.normal);

            groundHitInfo.isOnGround = true;
            groundHitInfo.isValidGround = !groundHitInfo.isOnLedgeEmptySide &&
                                          Vector3.Angle(groundHitInfo.surfaceNormal, /*Vector3.up*/up) < groundLimit;

            return groundHitInfo.isOnGround && groundHitInfo.isValidGround;
        }

        protected override void DrawGizmos()
        {
#if UNITY_EDITOR


            base.DrawGizmos();


            var radius = capsuleCollider.radius;

            var center = capsuleCollider.center;
            var offset = Mathf.Max(0.0f, capsuleCollider.height * 0.5f - radius);

            if (!Application.isPlaying)
                offset += castDistance;

            var color = new Color(0.5f, 1.0f, 0.6f);
            if (Application.isPlaying)
                color = isOnGround ? isValidGround ? Color.green : Color.blue : Color.red;

            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

            Gizmos.color = color;
            Gizmos.DrawWireSphere(center - Vector3.up * offset, radius * 1.01f);

            Gizmos.matrix = Matrix4x4.identity;


            Handles.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

            var standingOnLedge = isOnLedgeSolidSide || isOnLedgeEmptySide;
            if (standingOnLedge)
            {
                Handles.color = isOnLedgeSolidSide
                    ? new Color(0.0f, 1.0f, 0.0f, 0.25f)
                    : new Color(1.0f, 0.0f, 0.0f, 0.25f);

                Handles.DrawSolidDisc(Vector3.zero, Vector3.up, ledgeOffset);
            }

            Handles.matrix = Matrix4x4.identity;

#endif
        }

        #endregion
    }
}