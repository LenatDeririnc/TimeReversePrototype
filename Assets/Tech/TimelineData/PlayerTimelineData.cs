﻿using Tools.TimeLineStackTool;
using UnityEngine;

namespace TimelineData
{
    public class PlayerTimelineData : TimeLineElement
    {
        public PlayerTimelineData(float time) : base(time)
        {
        }
        
        public Vector3 playerPosition;
        public Quaternion playerRotation;
        public float cameraAngle;
    }
}