using Tools.TimeLineStackTool;
using UnityEngine;

namespace TimelineData
{
    public class RigidbodyTimelineData : TimeLineElement
    {
        public RigidbodyTimelineData(int entityId, float time) : base(entityId, time)
        {
        }
        
        public Vector3 Velocity;
        public Vector3 Position;
        public Quaternion Rotation;
    }
}