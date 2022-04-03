using Common;
using ECM.Components;
using ECM.Helpers;
using InputHandler;
using TimeSystem;
using UnityEngine;

namespace ECM.Controllers
{
    public class BasePlayerController : MonoBehaviour, IVelocity
    {
        public Vector3 Velocity()
        {
            return Vector3.ClampMagnitude(moveDirection, 1);
        }

        #region EDITOR EXPOSED FIELDS

        private PlayerMovementEditorFields PlayerMovementEditorFields;
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

        #endregion

        #region FIELDS

        private Vector3 _moveDirection;

        protected bool _canJump = true;
        protected bool _jump;
        protected bool _isJumping;

        protected bool _updateJumpTimer;
        protected float _jumpTimer;
        protected float _jumpButtonHeldDownTimer;
        protected float _jumpUngroundedTimer;

        protected int _midAirJumpCount;

        private bool _allowVerticalMovement;

        private bool _restoreVelocityOnResume = true;

        #endregion

        #region PROPERTIES

        public PlayerMovement movement { get; private set; }

        public Animator animator { get; set; }

        public RootMotionController rootMotionController { get; set; }

        public bool allowVerticalMovement
        {
            get => _allowVerticalMovement;
            set
            {
                _allowVerticalMovement = value;

                if (movement != null)
                    movement.fields.useGravity = !_allowVerticalMovement;
            }
        }

        public float jumpImpulse =>
            Mathf.Sqrt(2.0f * baseJumpHeight * movement.fields.gravity.magnitude);

        public bool applyRootMotion
        {
            get => animator != null && animator.applyRootMotion;
            set
            {
                if (animator != null)
                    animator.applyRootMotion = value;
            }
        }


        public bool jump
        {
            get => _jump;
            set
            {
                if (_jump && value == false)
                {
                    _canJump = true;
                    _jumpButtonHeldDownTimer = 0.0f;
                }


                _jump = value;
                if (_jump)
                    _jumpButtonHeldDownTimer += Time.deltaTime;
            }
        }


        public bool isJumping => _isJumping;


        public bool isGrounded => movement.isGrounded;


        public Vector3 moveDirection
        {
            get => _moveDirection;
            set => _moveDirection = Vector3.ClampMagnitude(value, 1.0f);
        }


        public bool isPaused { get; private set; }


        public bool restoreVelocityOnResume
        {
            get => _restoreVelocityOnResume;
            set => _restoreVelocityOnResume = value;
        }


        public bool crouch { get; set; }


        public bool isCrouching { get; protected set; }

        #endregion

        #region METHODS

        private void SetPause(bool value)
        {
            isPaused = value;
            movement.Pause(value, !value && restoreVelocityOnResume);
        }


        public void RotateTowards(Vector3 direction, bool onlyLateral = true)
        {
            movement.Rotate(direction, angularSpeed, onlyLateral);
        }


        public void RotateTowardsMoveDirection(bool onlyLateral = true)
        {
            RotateTowards(moveDirection, onlyLateral);
        }


        public void RotateTowardsVelocity(bool onlyLateral = true)
        {
            RotateTowards(movement.velocity, onlyLateral);
        }


        protected virtual void Jump()
        {
            if (isJumping)


                if (!movement.wasGrounded && movement.isGrounded)
                    _isJumping = false;


            if (movement.isGrounded)
                _jumpUngroundedTimer = 0.0f;
            else
                _jumpUngroundedTimer += Time.deltaTime;


            if (!_jump || !_canJump)
                return;


            if (_jumpButtonHeldDownTimer > jumpPreGroundedToleranceTime)
                return;


            if (!movement.isGrounded && _jumpUngroundedTimer > jumpPostGroundedToleranceTime)
                return;

            _canJump = false;
            _isJumping = true;
            _updateJumpTimer = true;


            _jumpUngroundedTimer = jumpPostGroundedToleranceTime;


            movement.ApplyVerticalImpulse(jumpImpulse);


            movement.DisableGrounding();
        }


        protected virtual void MidAirJump()
        {
            if (_midAirJumpCount > 0 && movement.isGrounded)
                _midAirJumpCount = 0;


            if (!_jump || !_canJump)
                return;


            if (movement.isGrounded)
                return;


            if (_midAirJumpCount >= maxMidAirJumps)
                return;

            _midAirJumpCount++;

            _canJump = false;
            _isJumping = true;
            _updateJumpTimer = true;


            movement.ApplyVerticalImpulse(jumpImpulse);


            movement.DisableGrounding();
        }


        protected virtual void UpdateJumpTimer()
        {
            if (!_updateJumpTimer)
                return;


            if (_jump && _jumpTimer < extraJumpTime)
            {
                var jumpProgress = _jumpTimer / extraJumpTime;


                var proportionalJumpPower = Mathf.Lerp(extraJumpPower, 0f, jumpProgress);
                movement.ApplyForce(transform.up * proportionalJumpPower, ForceMode.Acceleration);


                _jumpTimer = Mathf.Min(_jumpTimer + Time.deltaTime, extraJumpTime);
            }
            else
            {
                _jumpTimer = 0.0f;
                _updateJumpTimer = false;
            }
        }


        protected virtual void Crouch()
        {
            if (!canCrouch)
                return;


            if (crouch)
            {
                if (isCrouching)
                    return;


                movement.SetCapsuleHeight(crouchingHeight);


                isCrouching = true;
            }
            else
            {
                if (!isCrouching)
                    return;


                if (!movement.ClearanceCheck(standingHeight))
                    return;


                movement.SetCapsuleHeight(standingHeight);


                isCrouching = false;
            }
        }


        protected virtual Vector3 CalcDesiredVelocity()
        {
            if (useRootMotion && applyRootMotion)
                return rootMotionController.animVelocity;


            return moveDirection * speed;
        }


        protected virtual void Move()
        {
            var desiredVelocity = CalcDesiredVelocity();

            if (useRootMotion && applyRootMotion)
            {
                movement.Move(desiredVelocity, speed, !allowVerticalMovement);
            }
            else
            {
                var currentFriction = isGrounded ? groundFriction : airFriction;
                var currentBrakingFriction = useBrakingFriction ? brakingFriction : currentFriction;

                movement.Move(desiredVelocity, speed, acceleration, deceleration, currentFriction,
                    currentBrakingFriction, !allowVerticalMovement);
            }


            Jump();
            MidAirJump();
            UpdateJumpTimer();


            applyRootMotion = useRootMotion && movement.isGrounded;
        }


        protected virtual void Animate()
        {
        }


        protected virtual void UpdateRotation()
        {
            if (useRootMotion && applyRootMotion && rootMotionRotation)


                movement.rotation *= animator.deltaRotation;
            else


                RotateTowardsMoveDirection();
        }


        protected virtual void HandleInput()
        {
            var direction = InputHandlerComponent.Instance.ForwardMovement.Velocity();
            moveDirection = direction * Mathf.Clamp(TimeManagerComponent.TimeManager.timeSpeed, 0, 1);

            jump = InputHandlerComponent.Instance.jump;

            crouch = InputHandlerComponent.Instance.crouch;
        }

        #endregion

        #region MONOBEHAVIOUR

        public virtual void Awake()
        {
            var groundDetection = GetComponent<BaseGroundDetection>();
            var rigidbody = GetComponent<Rigidbody>();
            var collider = GetComponent<Collider>();

            movement = new PlayerMovement(transform, groundDetection, rigidbody, collider, PlayerMovementEditorFields)
            {
                platformUpdatesRotation = true
            };

            animator = GetComponentInChildren<Animator>();

            rootMotionController = GetComponentInChildren<RootMotionController>();

            InputHandlerComponent.OnPauseChanged += SetPause;
        }

        public virtual void FixedUpdate()
        {
            if (isPaused)
                return;


            movement?.FixedUpdate();

            Move();


            Crouch();
        }

        public virtual void Update()
        {
            HandleInput();


            if (isPaused)
                return;


            UpdateRotation();


            Animate();
        }

        #endregion
    }
}