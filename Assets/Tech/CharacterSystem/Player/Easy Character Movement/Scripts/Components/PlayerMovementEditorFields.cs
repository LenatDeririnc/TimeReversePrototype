using System;
using UnityEngine;

namespace ECM.Components
{
    [Serializable]
    public class PlayerMovementEditorFields
    {
        #region EDITOR_FIELDS
        
        [SerializeField] private float _maxLateralSpeed = 10.0f;
        [SerializeField] private float _maxRiseSpeed = 20.0f;
        [SerializeField] private float _maxFallSpeed = 20.0f;
        [SerializeField] private bool _useGravity = true;
        [SerializeField] private Vector3 _gravity = new Vector3(0.0f, -30.0f, 0.0f);
        [SerializeField] private bool _slideOnSteepSlope;
        [SerializeField] private float _slopeLimit = 45.0f;
        [SerializeField] private float _slideGravityMultiplier = 2.0f;
        [SerializeField] private bool _snapToGround = true;
        [Range(0.0f, 1.0f)] [SerializeField] private float _snapStrength = 0.5f;

        #endregion

        #region PROPERTIES

        public float maxLateralSpeed
        {
            get => _maxLateralSpeed;
            set => _maxLateralSpeed = Mathf.Max(0.0f, value);
        }

        public float maxRiseSpeed
        {
            get => _maxRiseSpeed;
            set => _maxRiseSpeed = Mathf.Max(0.0f, value);
        }

        public float maxFallSpeed
        {
            get => _maxFallSpeed;
            set => _maxFallSpeed = Mathf.Max(0.0f, value);
        }

        public bool useGravity
        {
            get => _useGravity;
            set => _useGravity = value;
        }

        public Vector3 gravity
        {
            get => _gravity;
            set => _gravity = value;
        }

        public bool slideOnSteepSlope
        {
            get => _slideOnSteepSlope;
            set => _slideOnSteepSlope = value;
        }

        public float slopeLimit
        {
            get => _slopeLimit;
            set => _slopeLimit = Mathf.Clamp(value, 0.0f, 89.0f);
        }

        public float slideGravityMultiplier
        {
            get => _slideGravityMultiplier;
            set => _slideGravityMultiplier = Mathf.Max(1.0f, value);
        }

        public bool snapToGround
        {
            get => _snapToGround;
            set => _snapToGround = value;
        }

        public float snapStrength
        {
            get => _snapStrength;
            set => _snapStrength = Mathf.Clamp01(value);
        }

        #endregion
    }
}