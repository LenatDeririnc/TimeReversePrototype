using System;
using System.Collections.Generic;
using UnityEngine;

namespace TimelineSystem
{
    public class PlayerTimelineComponent : MonoBehaviour
    {
        private PlayerTimeline _timeline;

        private void Awake()
        {
            _timeline = new PlayerTimeline();
        }
    }

    public class PlayerTimeline
    {
        private List<PlayerTimelineInfo> timelineData;
    }

    public struct PlayerTimelineInfo
    {
        public Vector3 position;
        public Quaternion rotation;
    }
}