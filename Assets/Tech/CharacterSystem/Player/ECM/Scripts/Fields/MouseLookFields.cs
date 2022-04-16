using System;
using UnityEngine;

namespace CharacterSystem.Player.ECM.Scripts.Fields
{
    [Serializable]
    public class MouseLookFields
    {
        public bool _lockCursor = true;
        public  KeyCode _unlockCursorKey = KeyCode.Escape;
        public  float _lateralSensitivity = 2.0f;
        public  float _verticalSensitivity = 2.0f;
        public  bool _smooth;
        public  float _smoothTime = 5.0f;
        public  bool _clampPitch = true;
        public  float _minPitchAngle = -90.0f;
        public  float _maxPitchAngle = 90.0f;
    }
}