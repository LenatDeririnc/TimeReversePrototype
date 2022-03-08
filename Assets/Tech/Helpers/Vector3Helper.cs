using UnityEngine;

namespace Helpers
{
    public static class Vector3Helper
    {
        public static float Angle360(Vector3 from, Vector3 to, Vector3 up)
        {
            var signedAngle = Vector3.SignedAngle(from, to, up);

            if (signedAngle < 0)
                signedAngle += 360;

            return signedAngle;
        }

        public static float Angle360(Vector3 from, Vector3 to)
        {
            return Angle360(from, to, Vector3.up);
        }
    }
}