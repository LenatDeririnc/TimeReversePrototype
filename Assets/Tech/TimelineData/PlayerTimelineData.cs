using System;
using Tools.TimeLineStackTool;
using UnityEngine;

namespace TimelineData
{
    public class PlayerTimelineData : TimeLineElement
    {
        public PlayerTimelineData(int entityId, float time) : base(entityId, time)
        {
        }
        
        public bool isDead;
        public Vector3 playerPosition;
        public Quaternion playerRotation;
        public float cameraAngle;
        public Vector3 playerRigidbodyImpulse;
    }
}