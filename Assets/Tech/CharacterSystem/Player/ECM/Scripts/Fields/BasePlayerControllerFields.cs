using System;
using UnityEngine;

namespace CharacterSystem.Player.ECM.Scripts.Fields
{
    [Serializable]
    public class BasePlayerControllerFields
    {
        public float speed = 5.0f;
        public float angularSpeed = 540.0f;
        public float acceleration = 50.0f;
        public float deceleration = 20.0f;
        public float groundFriction = 8f;
        public bool useBrakingFriction;
        public float brakingFriction = 8f;
        public float airFriction;
        public bool canCrouch = true;
        public float standingHeight = 2.0f;
        public float crouchingHeight = 1.0f;
        public float baseJumpHeight = 1.5f;
        public float extraJumpTime = 0.5f;
        public float extraJumpPower = 25.0f;
        public float jumpPreGroundedToleranceTime = 0.15f;
        public float jumpPostGroundedToleranceTime = 0.15f;
        public float maxMidAirJumps = 1;
        public bool useRootMotion;
        public bool rootMotionRotation;
        
        public float maxLateralSpeed = 10.0f;
        public float maxRiseSpeed = 20.0f;
        public float maxFallSpeed = 20.0f;
        public bool useGravity = true;
        public Vector3 gravity = new Vector3(0.0f, -30.0f, 0.0f);
        public bool slideOnSteepSlope;
        public float slopeLimit = 45.0f;
        public float slideGravityMultiplier = 2.0f;
        public bool snapToGround = true;
        [Range(0.0f, 1.0f)] public float snapStrength = 0.5f;
    }
}