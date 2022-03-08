using System;
using UnityEngine;

namespace Helpers
{
    [Serializable]
    public struct Sector
    {
        public Transform transform;
        public float angle;
        public float rotation;

        public Vector2 Edges => new Vector2(rotation - angle, rotation + angle);


        public Sector(float angle, Transform transform, float rotation)
        {
            this.angle = angle;
            this.transform = transform;
            this.rotation = rotation;
        }

        public float GetAngle(Vector3 vector3)
        {
            return Vector3Helper.Angle360(vector3, transform.forward, transform.up);
        }

        public static bool Intersection(Vector3 vector3, Sector sector)
        {
            var angle = sector.GetAngle(vector3);

            return angle > sector.Edges.x && angle < sector.Edges.y;
        }
    }
}