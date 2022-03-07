using System;
using TimelineSystem;
using UnityEngine;

namespace CharacterSystem
{
    public class PlayerComponent : MonoBehaviour
    {
        public Transform Transform { get; private set; }

        private void Awake()
        {
            Transform = transform;
        }

        public void SetTransformData(PlayerTimelineInfo data)
        {
            Transform.position = data.position;
            Transform.rotation = data.rotation;
        }

        public PlayerTimelineInfo GetPlayerInfo()
        {
            var info = new PlayerTimelineInfo()
            {
                position = Transform.position,
                rotation = Transform.rotation,
            };

            return info;
        }
    }
}