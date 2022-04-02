using System;
using UnityEngine;

namespace ECM.Components
{
    /// <summary>
    ///     Character Movement.
    ///     'CharacterMovement' is the core of the ECM system and is responsible to perform
    ///     all the heavy work to move a character (a.k.a. Character motor),
    ///     such as apply forces, impulses, constraints, platforms interaction, etc.
    ///     This is analogous to the Unity's character controller, but unlike the Unity character controller,
    ///     this make use of Rigidbody physics.
    ///     The controller (eg: 'BaseCharacterController') determines how the Character should be moved,
    ///     such as in response from user input, AI, animation, etc.
    ///     and feed this information to the 'CharacterMovement' component, which perform the movement.
    /// </summary>
    [Serializable]
    public class PlayerMovementEditorFields
    {
        #region EDITOR_FIELDS

        [Header("Speed Limiters")]
        [Tooltip("The maximum lateral speed this character can move, " +
                 "including movement from external forces like sliding, collisions, etc.")]
        [SerializeField]
        private float _maxLateralSpeed = 10.0f;

        [Tooltip("The maximum rising speed, " +
                 "including movement from external forces like sliding, collisions, etc.")]
        [SerializeField]
        private float _maxRiseSpeed = 20.0f;

        [Tooltip("The maximum falling speed, " +
                 "including movement from external forces like sliding, collisions, etc.")]
        [SerializeField]
        private float _maxFallSpeed = 20.0f;

        [Header("Gravity")]
        [Tooltip("Enable / disable character's custom gravity." +
                 "If enabled the character will be affected by this gravity force.")]
        [SerializeField]
        private bool _useGravity = true;

        [Tooltip("The gravity applied to this character.")]
        private Vector3 _gravity = new Vector3(0.0f, -30.0f, 0.0f);

        [Header("Slopes")] [Tooltip("Should the character slide down of a steep slope?")] [SerializeField]
        private bool _slideOnSteepSlope;

        [Tooltip("The maximum angle (in degrees) for a walkable slope.")] [SerializeField]
        private float _slopeLimit = 45.0f;

        [Tooltip("The amount of gravity to be applied when sliding off a steep slope.")] [SerializeField]
        private float _slideGravityMultiplier = 2.0f;

        [Header("Ground-Snap")]
        [Tooltip("When enabled, will force the character to safely follow the walkable 'ground' geometry.")]
        [SerializeField]
        private bool _snapToGround = true;

        [Tooltip("A tolerance of how close to the 'ground' maintain the character.\n" +
                 "0 == no snap at all, 1 == 100% stick to ground.")]
        [Range(0.0f, 1.0f)]
        [SerializeField]
        private float _snapStrength = 0.5f;

        #endregion

        #region PROPERTIES

        /// <summary>
        ///     The maximum lateral speed this character can move,
        ///     including movement from external forces like sliding, collisions, etc.
        /// </summary>

        public float maxLateralSpeed
        {
            get => _maxLateralSpeed;
            set => _maxLateralSpeed = Mathf.Max(0.0f, value);
        }

        /// <summary>
        ///     The maximum rising speed,
        ///     including movement from external forces like sliding, collisions, etc.
        /// </summary>

        public float maxRiseSpeed
        {
            get => _maxRiseSpeed;
            set => _maxRiseSpeed = Mathf.Max(0.0f, value);
        }

        /// <summary>
        ///     The maximum fall speed,
        ///     including movement from external forces like sliding, collisions, etc.
        /// </summary>

        public float maxFallSpeed
        {
            get => _maxFallSpeed;
            set => _maxFallSpeed = Mathf.Max(0.0f, value);
        }

        /// <summary>
        ///     Enable / disable character's gravity.
        ///     If enabled the character will be affected by its custom gravity force.
        /// </summary>

        public bool useGravity
        {
            get => _useGravity;
            set => _useGravity = value;
        }

        /// <summary>
        ///     The amount of gravity to be applied to this character.
        ///     We apply gravity manually for more tuning control.
        /// </summary>

        public Vector3 gravity
        {
            get => _gravity;
            set => _gravity = value;
        }

        /// <summary>
        ///     Should the character slide down of a steep slope?
        /// </summary>

        public bool slideOnSteepSlope
        {
            get => _slideOnSteepSlope;
            set => _slideOnSteepSlope = value;
        }

        /// <summary>
        ///     The maximum angle (in degrees) the slope needs to be before the character starts to slide.
        /// </summary>

        public float slopeLimit
        {
            get => _slopeLimit;
            set => _slopeLimit = Mathf.Clamp(value, 0.0f, 89.0f);
        }

        /// <summary>
        ///     The percentage of gravity that will be applied to the slide.
        /// </summary>

        public float slideGravityMultiplier
        {
            get => _slideGravityMultiplier;
            set => _slideGravityMultiplier = Mathf.Max(1.0f, value);
        }

        /// <summary>
        ///     If enabled, will prevent the character leaving the ground.
        ///     This will cause the character to safely follow the geometry of the ground.
        /// </summary>

        public bool snapToGround
        {
            get => _snapToGround;
            set => _snapToGround = value;
        }

        /// <summary>
        ///     The strength of snap to ground.
        ///     0 == no snap at all, 1 == 100% stick to ground.
        /// </summary>

        public float snapStrength
        {
            get => _snapStrength;
            set => _snapStrength = Mathf.Clamp01(value);
        }

        #endregion
    }
}