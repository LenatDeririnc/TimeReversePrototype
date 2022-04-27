using CharacterSystem.Player.ECM.Scripts.Common;
using CharacterSystem.Player.ECM.Scripts.Components.GroundDetection;
using CharacterSystem.Player.ECM.Scripts.Fields;
using CharacterSystem.Player.ECM.Scripts.Helpers;
using ECS;
using UnityEngine; // using InputHandler;

namespace CharacterSystem.Player.ECM.Scripts.Controllers
{
    public class BasePlayerController : IPlayerMovementForMouse
    {
        protected readonly PlayerModel _model;
        protected readonly PlayerController _playerController;
        protected BasePlayerControllerFields _baseFields;

        protected BasePlayerController(PlayerModel playerModel, PlayerController playerController)
        {
            _model = playerModel;
            _playerController = playerController;

            _baseFields = playerModel.BasePlayerControllerFields;
            
            animator = _model.Animator;
            rootMotionController = _playerController.RootMotionController;
            // InputHandlerComponent.OnPauseChanged += SetPause;

            InitPlayerMovement(playerModel, playerController);
        }
        
        private void InitPlayerMovement(PlayerModel model, PlayerController controller)
        {
            pGroundDetection = controller.BaseGroundDetection;
            if (pGroundDetection == null)
            {
                Debug.LogError(
                    string.Format(
                        "CharacterMovement: No 'GroundDetection' found for '{0}' game object.\n" +
                        "Please add a 'GroundDetection' component to '{0}' game object",
                        model.transform.name));

                return;
            }

            _referenceCastDistance = pGroundDetection.castDistance;

            cachedRigidbody = model.rigidBody;
            if (cachedRigidbody == null)
            {
                Debug.LogError(
                    string.Format(
                        "CharacterMovement: No 'Rigidbody' found for '{0}' game object.\n" +
                        "Please add a 'Rigidbody' component to '{0}' game object",
                        model.transform.name));

                return;
            }

            cachedRigidbody.useGravity = false;
            cachedRigidbody.isKinematic = false;
            cachedRigidbody.freezeRotation = true;


            var aCollider = model.capsuleCollider;
            if (aCollider == null)
                return;

            var physicMaterial = aCollider.sharedMaterial;
            if (physicMaterial != null)
                return;

            physicMaterial = new PhysicMaterial("Frictionless")
            {
                dynamicFriction = 0.0f,
                staticFriction = 0.0f,
                bounciness = 0.0f,
                frictionCombine = PhysicMaterialCombine.Multiply,
                bounceCombine = PhysicMaterialCombine.Average
            };

            aCollider.material = physicMaterial;

            Debug.LogWarning(
                string.Format(
                    "CharacterMovement: No 'PhysicMaterial' found for '{0}'s Collider, a frictionless one has been created and assigned.\n" +
                    "Please add a Frictionless 'PhysicMaterial' to '{0}' game object.",
                    model.transform.name));
        }

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

        public Animator animator { get; set; }

        public RootMotionController rootMotionController { get; set; }

        public bool allowVerticalMovement
        {
            get => _allowVerticalMovement;
            set
            {
                _allowVerticalMovement = value;
                _baseFields.useGravity = !_allowVerticalMovement;
            }
        }

        public float jumpImpulse =>
            Mathf.Sqrt(2.0f * _baseFields.baseJumpHeight * _baseFields.gravity.magnitude);

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

        #region FIELDS

        private static readonly Collider[] OverlappedColliders = new Collider[8];


        private Vector3 _normal;

        private float _referenceCastDistance;

        private bool _forceUnground;
        private float _forceUngroundTimer;
        private bool _performGroundDetection = true;

        private Vector3 _savedVelocity;
        private Vector3 _savedAngularVelocity;

        #endregion

        #region PROPERTIES

        public Transform transform => _model.transform;
        public CapsuleCollider capsuleCollider => pGroundDetection.capsuleCollider;
        private BaseGroundDetection pGroundDetection { get; set; }
        public Vector3 groundPoint => pGroundDetection.groundPoint;
        public Vector3 groundNormal => pGroundDetection.groundNormal;
        public Vector3 surfaceNormal => pGroundDetection.surfaceNormal;
        public float groundDistance => pGroundDetection.groundDistance;
        public Collider groundCollider => pGroundDetection.groundCollider;
        public Rigidbody groundRigidbody => pGroundDetection.groundRigidbody;
        public bool isGrounded => pGroundDetection.isOnGround && pGroundDetection.isValidGround;
        public bool wasGrounded =>
            pGroundDetection.prevGroundHit.isOnGround && pGroundDetection.prevGroundHit.isValidGround;
        public bool isOnGround => pGroundDetection.isOnGround;
        public bool wasOnGround => pGroundDetection.prevGroundHit.isOnGround;
        public bool isValidGround => pGroundDetection.isValidGround;
        public bool isOnPlatform { get; private set; }
        public bool isOnLedgeSolidSide => pGroundDetection.isOnLedgeSolidSide;
        public bool isOnLedgeEmptySide => pGroundDetection.isOnLedgeEmptySide;
        public float ledgeDistance => pGroundDetection.ledgeDistance;
        public bool isOnStep => pGroundDetection.isOnStep;
        public float stepHeight => pGroundDetection.stepHeight;
        public bool isOnSlope => pGroundDetection.isOnSlope;
        public float groundAngle => pGroundDetection.groundAngle;
        public bool isValidSlope => !_baseFields.slideOnSteepSlope || groundAngle < _baseFields.slopeLimit;
        public bool isSliding { get; private set; }
        public Vector3 platformVelocity { get; private set; }
        public Vector3 platformAngularVelocity { get; private set; }
        public bool platformUpdatesRotation { get; set; } = true;
        public Vector3 velocity
        {
            get => cachedRigidbody.velocity - platformVelocity;
            set => cachedRigidbody.velocity = value + platformVelocity;
        }
        public float forwardSpeed => Vector3.Dot(velocity, transform.forward);
        public Quaternion rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }
        public GroundHit groundHit => pGroundDetection.groundHit;
        public GroundHit prevGroundHit => pGroundDetection.prevGroundHit;
        public LayerMask groundMask => pGroundDetection.groundMask;
        public LayerMask overlapMask => pGroundDetection.overlapMask;
        public QueryTriggerInteraction triggerInteraction => pGroundDetection.triggerInteraction;
        public Rigidbody cachedRigidbody { get; private set; }

        #endregion
        
        #region METHODS

        private void SetPause(bool value)
        {
            isPaused = value;
            Pause(value, !value && restoreVelocityOnResume);
        }


        public void RotateTowards(Vector3 direction, bool onlyLateral = true)
        {
            Rotate(direction, _baseFields.angularSpeed, onlyLateral);
        }


        public void RotateTowardsMoveDirection(bool onlyLateral = true)
        {
            RotateTowards(moveDirection, onlyLateral);
        }


        public void RotateTowardsVelocity(bool onlyLateral = true)
        {
            RotateTowards(velocity, onlyLateral);
        }


        protected virtual void Jump()
        {
            if (isJumping)


                if (!wasGrounded && isGrounded)
                    _isJumping = false;


            if (isGrounded)
                _jumpUngroundedTimer = 0.0f;
            else
                _jumpUngroundedTimer += Time.deltaTime;


            if (!_jump || !_canJump)
                return;


            if (_jumpButtonHeldDownTimer > _baseFields.jumpPreGroundedToleranceTime)
                return;


            if (!isGrounded && _jumpUngroundedTimer > _baseFields.jumpPostGroundedToleranceTime)
                return;

            _canJump = false;
            _isJumping = true;
            _updateJumpTimer = true;


            _jumpUngroundedTimer = _baseFields.jumpPostGroundedToleranceTime;


            ApplyVerticalImpulse(jumpImpulse);


            DisableGrounding();
        }


        protected virtual void MidAirJump()
        {
            if (_midAirJumpCount > 0 && isGrounded)
                _midAirJumpCount = 0;


            if (!_jump || !_canJump)
                return;


            if (isGrounded)
                return;


            if (_midAirJumpCount >= _baseFields.maxMidAirJumps)
                return;

            _midAirJumpCount++;

            _canJump = false;
            _isJumping = true;
            _updateJumpTimer = true;


            ApplyVerticalImpulse(jumpImpulse);


            DisableGrounding();
        }


        protected virtual void UpdateJumpTimer()
        {
            if (!_updateJumpTimer)
                return;


            if (_jump && _jumpTimer < _baseFields.extraJumpTime)
            {
                var jumpProgress = _jumpTimer / _baseFields.extraJumpTime;


                var proportionalJumpPower = Mathf.Lerp(_baseFields.extraJumpPower, 0f, jumpProgress);
                ApplyForce(transform.up * proportionalJumpPower, ForceMode.Acceleration);


                _jumpTimer = Mathf.Min(_jumpTimer + Time.deltaTime, _baseFields.extraJumpTime);
            }
            else
            {
                _jumpTimer = 0.0f;
                _updateJumpTimer = false;
            }
        }


        protected virtual void Crouch()
        {
            if (!_baseFields.canCrouch)
                return;


            if (crouch)
            {
                if (isCrouching)
                    return;


                SetCapsuleHeight(_baseFields.crouchingHeight);


                isCrouching = true;
            }
            else
            {
                if (!isCrouching)
                    return;


                if (!ClearanceCheck(_baseFields.standingHeight))
                    return;


                SetCapsuleHeight(_baseFields.standingHeight);


                isCrouching = false;
            }
        }


        protected virtual Vector3 CalcDesiredVelocity()
        {
            if (_baseFields.useRootMotion && applyRootMotion)
                return rootMotionController.animVelocity;


            return moveDirection * _baseFields.speed;
        }


        protected virtual void Move()
        {
            var desiredVelocity = CalcDesiredVelocity();

            if (_baseFields.useRootMotion && applyRootMotion)
            {
                Move(desiredVelocity, _baseFields.speed, !allowVerticalMovement);
            }
            else
            {
                var currentFriction = isGrounded ? _baseFields.groundFriction : _baseFields.airFriction;
                var currentBrakingFriction = _baseFields.useBrakingFriction ? _baseFields.brakingFriction : currentFriction;

                Move(desiredVelocity, _baseFields.speed, _baseFields.acceleration, _baseFields.deceleration, currentFriction,
                    currentBrakingFriction, !allowVerticalMovement);
            }


            Jump();
            MidAirJump();
            UpdateJumpTimer();


            applyRootMotion = _baseFields.useRootMotion && isGrounded;
        }


        protected virtual void UpdateRotation()
        {
            if (_baseFields.useRootMotion && applyRootMotion && _baseFields.rootMotionRotation)
            {
                rotation *= animator.deltaRotation;
            }
            else
            {
                RotateTowardsMoveDirection();
            }
        }


        public virtual void HandleInput(InputData data)
        {
            moveDirection = data.direction * Mathf.Clamp(EcsBootstrapper.Contexts.time.globalTimeSpeed.Value, 0, 1);
            jump = data.jump;
            crouch = data.crouch;
        }

        #endregion
        
        

        #region METHODS

        public void Pause(bool pause, bool restoreVelocity = true)
        {
            if (pause)
            {
                _savedVelocity = cachedRigidbody.velocity;
                _savedAngularVelocity = cachedRigidbody.angularVelocity;

                cachedRigidbody.isKinematic = true;
            }
            else
            {
                cachedRigidbody.isKinematic = false;

                if (restoreVelocity)
                {
                    cachedRigidbody.AddForce(_savedVelocity, ForceMode.VelocityChange);
                    cachedRigidbody.AddTorque(_savedAngularVelocity, ForceMode.VelocityChange);
                }
                else
                {
                    var zero = Vector3.zero;

                    cachedRigidbody.AddForce(zero, ForceMode.VelocityChange);
                    cachedRigidbody.AddTorque(zero, ForceMode.VelocityChange);
                }

                cachedRigidbody.WakeUp();
            }
        }


        public void SetCapsuleDimensions(Vector3 capsuleCenter, float capsuleRadius, float capsuleHeight)
        {
            capsuleCollider.center = capsuleCenter;
            capsuleCollider.radius = capsuleRadius;
            capsuleCollider.height = Mathf.Max(capsuleRadius * 0.5f, capsuleHeight);
        }


        public void SetCapsuleDimensions(float capsuleRadius, float capsuleHeight)
        {
            capsuleCollider.center = new Vector3(0.0f, capsuleHeight * 0.5f, 0.0f);
            capsuleCollider.radius = capsuleRadius;
            capsuleCollider.height = Mathf.Max(capsuleRadius * 0.5f, capsuleHeight);
        }


        public void SetCapsuleHeight(float capsuleHeight)
        {
            capsuleHeight = Mathf.Max(capsuleCollider.radius * 2.0f, capsuleHeight);

            capsuleCollider.center = new Vector3(0.0f, capsuleHeight * 0.5f, 0.0f);
            capsuleCollider.height = capsuleHeight;
        }


        private void OverlapCapsule(Vector3 bottom, Vector3 top, float radius, out int overlapCount,
            LayerMask overlappingMask, QueryTriggerInteraction queryTriggerInteraction)
        {
            var colliderCount = Physics.OverlapCapsuleNonAlloc(bottom, top, radius, OverlappedColliders,
                overlappingMask, queryTriggerInteraction);

            overlapCount = colliderCount;
            for (var i = 0; i < colliderCount; i++)
            {
                var overlappedCollider = OverlappedColliders[i];
                if (overlappedCollider != null && overlappedCollider != capsuleCollider)
                    continue;

                if (i < --overlapCount)
                    OverlappedColliders[i] = OverlappedColliders[overlapCount];
            }
        }


        public Collider[] OverlapCapsule(Vector3 position, Quaternion rotation, out int overlapCount,
            LayerMask overlapMask, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Ignore)
        {
            var center = capsuleCollider.center;
            var radius = capsuleCollider.radius;

            var height = capsuleCollider.height * 0.5f - radius;

            var topSphereCenter = center + Vector3.up * height;
            var bottomSphereCenter = center - Vector3.up * height;

            var top = position + rotation * topSphereCenter;
            var bottom = position + rotation * bottomSphereCenter;

            var colliderCount = Physics.OverlapCapsuleNonAlloc(bottom, top, radius, OverlappedColliders, overlapMask,
                queryTriggerInteraction);

            overlapCount = colliderCount;
            for (var i = 0; i < colliderCount; i++)
            {
                var overlappedCollider = OverlappedColliders[i];
                if (overlappedCollider != null && overlappedCollider != capsuleCollider)
                    continue;

                if (i < --overlapCount)
                    OverlappedColliders[i] = OverlappedColliders[overlapCount];
            }

            return OverlappedColliders;
        }


        public bool ClearanceCheck(float clearanceHeight)
        {
            const float kTolerance = 0.01f;


            var radius = Mathf.Max(kTolerance, capsuleCollider.radius - kTolerance);

            var height = Mathf.Max(radius * 2.0f + kTolerance, clearanceHeight - kTolerance);
            var halfHeight = height * 0.5f;

            var center = new Vector3(0.0f, halfHeight, 0.0f);

            var p = transform.position;
            var q = transform.rotation;

            var up = q * Vector3.up;

            var localBottom = center - up * Mathf.Max(0.0f, halfHeight - kTolerance) + up * radius;
            var localTop = center + up * halfHeight - up * radius;

            var bottom = p + q * localBottom;
            var top = p + q * localTop;


            int overlapCount;
            OverlapCapsule(bottom, top, radius, out overlapCount, overlapMask, triggerInteraction);


            return overlapCount == 0;
        }


        private void OverlapRecovery(ref Vector3 probingPosition, Quaternion probingRotation)
        {
            int overlapCount;
            var overlappedColliders =
                pGroundDetection.OverlapCapsule(probingPosition, probingRotation, out overlapCount);

            for (var i = 0; i < overlapCount; i++)
            {
                var overlappedCollider = overlappedColliders[i];

                var overlappedColliderRigidbody = overlappedCollider.attachedRigidbody;
                if (overlappedColliderRigidbody != null)
                    continue;

                var overlappedColliderTransform = overlappedCollider.transform;

                float distance;
                Vector3 direction;
                if (!Physics.ComputePenetration(capsuleCollider, probingPosition, probingRotation, overlappedCollider,
                    overlappedColliderTransform.position, overlappedColliderTransform.rotation, out direction,
                    out distance))
                    continue;

                probingPosition += direction * distance;
            }
        }


        public bool ComputeGroundHit(Vector3 probingPosition, Quaternion probingRotation, out GroundHit groundHitInfo,
            float scanDistance = Mathf.Infinity)
        {
            groundHitInfo = new GroundHit();
            return pGroundDetection.ComputeGroundHit(probingPosition, probingRotation, ref groundHitInfo, scanDistance);
        }


        public bool ComputeGroundHit(out GroundHit hitInfo, float scanDistance = Mathf.Infinity)
        {
            var p = transform.position;
            var q = transform.rotation;

            return ComputeGroundHit(p, q, out hitInfo, scanDistance);
        }


        public void Rotate(Vector3 direction, float angularSpeed, bool onlyLateral = true)
        {
            if (onlyLateral)
                direction = Vector3.ProjectOnPlane(direction, transform.up);

            if (direction.sqrMagnitude < 0.0001f)
                return;

            var targetRotation = Quaternion.LookRotation(direction, transform.up);
            var newRotation = Quaternion.Slerp(cachedRigidbody.rotation, targetRotation,
                angularSpeed * Mathf.Deg2Rad * Time.deltaTime);

            cachedRigidbody.MoveRotation(newRotation);
        }


        public void ApplyDrag(float drag, bool onlyLateral = true)
        {
            var up = transform.up;
            var v = onlyLateral ? Vector3.ProjectOnPlane(velocity, up) : velocity;

            var d = -drag * v.magnitude * v;

            cachedRigidbody.AddForce(d, ForceMode.Acceleration);
        }


        public void ApplyForce(Vector3 force, ForceMode forceMode = ForceMode.Force)
        {
            cachedRigidbody.AddForce(force, forceMode);
        }


        public void ApplyVerticalImpulse(float impulse)
        {
            Vector3 up = transform.up;
            cachedRigidbody.velocity = Vector3.ProjectOnPlane(cachedRigidbody.velocity, up) + up * impulse;
        }


        public void ApplyImpulse(Vector3 impulse)
        {
            cachedRigidbody.velocity += impulse - Vector3.Project(cachedRigidbody.velocity, transform.up);
        }


        public void DisableGrounding(float time = 0.1f)
        {
            _forceUnground = true;
            _forceUngroundTimer = time;

            pGroundDetection.castDistance = 0.0f;
        }


        public void SetGroundDetection(bool value)
        {
            _performGroundDetection = value;
        }

        private void ResetGroundInfo()
        {
            pGroundDetection.ResetGroundInfo();

            isSliding = false;

            isOnPlatform = false;
            platformVelocity = Vector3.zero;
            platformAngularVelocity = Vector3.zero;

            _normal = transform.up;
        }


        private void DetectGround()
        {
            ResetGroundInfo();


            if (_performGroundDetection)
            {
                if (_forceUnground || _forceUngroundTimer > 0.0f)
                {
                    _forceUnground = false;
                    _forceUngroundTimer -= Time.deltaTime;
                }
                else
                {
                    pGroundDetection.DetectGround();
                    pGroundDetection.castDistance = isGrounded ? _referenceCastDistance : 0.0f;
                }
            }


            if (!isOnGround)
                return;


            var up = transform.up;

            if (isValidGround)
                _normal = isOnLedgeSolidSide ? up : pGroundDetection.groundNormal;
            else


                _normal = Vector3.Cross(Vector3.Cross(up, pGroundDetection.groundNormal), up).normalized;


            var otherRigidbody = groundRigidbody;
            if (otherRigidbody == null)
                return;

            if (otherRigidbody.isKinematic)
            {
                isOnPlatform = true;
                platformVelocity = otherRigidbody.GetPointVelocity(groundPoint);
                platformAngularVelocity = Vector3.Project(otherRigidbody.angularVelocity, up);
            }
            else
            {
                _normal = up;
            }
        }


        private void PreventGroundPenetration()
        {
            if (isOnGround)
                return;


            var v = velocity;

            var speed = v.magnitude;

            var direction = speed > 0.0f ? v / speed : Vector3.zero;
            var distance = speed * Time.deltaTime;

            RaycastHit hitInfo;
            if (!pGroundDetection.FindGround(direction, out hitInfo, distance))
                return;


            var remainingDistance = distance - hitInfo.distance;
            if (remainingDistance <= 0.0f)
                return;


            var velocityToGround = direction * (hitInfo.distance / Time.deltaTime);


            var up = transform.up;
            var remainingLateralVelocity = Vector3.ProjectOnPlane(v - velocityToGround, up);


            remainingLateralVelocity = MathLibrary.GetTangent(remainingLateralVelocity, hitInfo.normal, up) *
                                       remainingLateralVelocity.magnitude;


            var newVelocity = velocityToGround + remainingLateralVelocity;


            cachedRigidbody.velocity = newVelocity;


            pGroundDetection.castDistance = _referenceCastDistance;
        }


        private void ApplyMovement(Vector3 desiredVelocity, float maxDesiredSpeed, bool onlyLateral)
        {
            var up = transform.up;

            if (onlyLateral)
                desiredVelocity = Vector3.ProjectOnPlane(desiredVelocity, up);


            if (isGrounded)
            {
                if (!_baseFields.slideOnSteepSlope || groundAngle < _baseFields.slopeLimit)
                {
                    desiredVelocity = MathLibrary.GetTangent(desiredVelocity, _normal, up) *
                                      Mathf.Min(desiredVelocity.magnitude, maxDesiredSpeed);

                    velocity += desiredVelocity - velocity;
                }
                else
                {
                    isSliding = true;

                    velocity += _baseFields.gravity * (_baseFields.slideGravityMultiplier * Time.deltaTime);
                }
            }
            else
            {
                if (isOnGround)
                {
                    var isBraking = desiredVelocity.sqrMagnitude < 0.000001f;
                    if (isBraking && onlyLateral)
                    {
                        desiredVelocity = velocity;
                    }
                    else
                    {
                        if (Vector3.Dot(desiredVelocity, _normal) <= 0.0f)
                        {
                            var speedLimit = Mathf.Min(desiredVelocity.magnitude, maxDesiredSpeed);

                            var lateralVelocity = Vector3.ProjectOnPlane(velocity, up);

                            desiredVelocity = Vector3.ProjectOnPlane(desiredVelocity, _normal) +
                                              Vector3.Project(lateralVelocity, _normal);

                            desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, speedLimit);
                        }
                    }
                }


                velocity += onlyLateral
                    ? Vector3.ProjectOnPlane(desiredVelocity - velocity, up)
                    : desiredVelocity - velocity;


                if (_baseFields.useGravity)
                    velocity += _baseFields.gravity * Time.deltaTime;
            }


            if (!isOnStep)
                return;

            var dot = Vector3.Dot(velocity, groundPoint - transform.position);
            if (dot <= 0.0f)
                return;

            var angle = Mathf.Abs(90.0f - Vector3.Angle(up, velocity));
            if (angle < 75.0f)
                return;

            var factor = Mathf.Lerp(1.0f, 0.0f, Mathf.InverseLerp(75.0f, 90.0f, angle));
            factor = factor * (2.0f - factor);

            velocity *= factor;
        }


        private void ApplyGroundMovement(Vector3 desiredVelocity, float maxDesiredSpeed, float acceleration,
            float deceleration, float friction, float brakingFriction)
        {
            var up = transform.up;
            var deltaTime = Time.deltaTime;


            if (!_baseFields.slideOnSteepSlope || groundAngle < _baseFields.slopeLimit)
            {
                var v = wasGrounded ? velocity : Vector3.ProjectOnPlane(velocity, up);


                var desiredSpeed = desiredVelocity.magnitude;
                var speedLimit = desiredSpeed > 0.0f ? Mathf.Min(desiredSpeed, maxDesiredSpeed) : maxDesiredSpeed;


                var desiredDirection = MathLibrary.GetTangent(desiredVelocity, _normal, up);
                var desiredAcceleration = desiredDirection * (acceleration * deltaTime);

                if (desiredAcceleration.isZero() || v.isExceeding(speedLimit))
                {
                    v = MathLibrary.GetTangent(v, _normal, up) * v.magnitude;


                    v = v * Mathf.Clamp01(1f - brakingFriction * deltaTime);


                    v = Vector3.MoveTowards(v, desiredVelocity, deceleration * deltaTime);
                }
                else
                {
                    v = MathLibrary.GetTangent(v, _normal, up) * v.magnitude;


                    v = v - (v - desiredDirection * v.magnitude) * Mathf.Min(friction * deltaTime, 1.0f);


                    v = Vector3.ClampMagnitude(v + desiredAcceleration, speedLimit);
                }


                velocity += v - velocity;
            }
            else
            {
                isSliding = true;

                velocity += _baseFields.gravity * (_baseFields.slideGravityMultiplier * Time.deltaTime);
            }
        }


        private void ApplyAirMovement(Vector3 desiredVelocity, float maxDesiredSpeed, float acceleration,
            float deceleration, float friction, float brakingFriction, bool onlyLateral = true)
        {
            var up = transform.up;
            var v = onlyLateral ? Vector3.ProjectOnPlane(velocity, up) : velocity;


            if (onlyLateral)
                desiredVelocity = Vector3.ProjectOnPlane(desiredVelocity, up);


            if (isOnGround)


                if (Vector3.Dot(desiredVelocity, _normal) <= 0.0f)
                {
                    var maxLength = Mathf.Min(desiredVelocity.magnitude, maxDesiredSpeed);

                    var lateralVelocity = Vector3.ProjectOnPlane(velocity, up);

                    desiredVelocity = Vector3.ProjectOnPlane(desiredVelocity, _normal) +
                                      Vector3.Project(lateralVelocity, _normal);

                    desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxLength);
                }


            var desiredSpeed = desiredVelocity.magnitude;
            var speedLimit = desiredSpeed > 0.0f ? Mathf.Min(desiredSpeed, maxDesiredSpeed) : maxDesiredSpeed;


            var deltaTime = Time.deltaTime;

            var desiredDirection = desiredSpeed > 0.0f ? desiredVelocity / desiredSpeed : Vector3.zero;
            var desiredAcceleration = desiredDirection * (acceleration * deltaTime);

            if (desiredAcceleration.isZero() || v.isExceeding(speedLimit))
            {
                if (isOnGround && onlyLateral)
                {
                }
                else
                {
                    v = v * Mathf.Clamp01(1f - brakingFriction * deltaTime);


                    v = Vector3.MoveTowards(v, desiredVelocity, deceleration * deltaTime);
                }
            }
            else
            {
                v = v - (v - desiredDirection * v.magnitude) * Mathf.Min(friction * deltaTime, 1.0f);


                v = Vector3.ClampMagnitude(v + desiredAcceleration, speedLimit);
            }


            if (onlyLateral)
                velocity += Vector3.ProjectOnPlane(v - velocity, up);
            else
                velocity += v - velocity;


            if (_baseFields.useGravity)
                velocity += _baseFields.gravity * Time.deltaTime;
        }


        private void ApplyMovement(Vector3 desiredVelocity, float maxDesiredSpeed, float acceleration,
            float deceleration, float friction, float brakingFriction, bool onlyLateral)
        {
            if (isGrounded)
                ApplyGroundMovement(desiredVelocity, maxDesiredSpeed, acceleration, deceleration, friction,
                    brakingFriction);
            else
                ApplyAirMovement(desiredVelocity, maxDesiredSpeed, acceleration, deceleration, friction,
                    brakingFriction, onlyLateral);


            if (!isOnStep)
                return;

            var dot = Vector3.Dot(velocity, groundPoint - transform.position);
            if (dot <= 0.0f)
                return;

            var angle = Mathf.Abs(90.0f - Vector3.Angle(transform.up, velocity));
            if (angle < 75.0f)
                return;

            var factor = Mathf.Lerp(1.0f, 0.0f, Mathf.InverseLerp(75.0f, 90.0f, angle));
            factor = factor * (2.0f - factor);

            velocity *= factor;
        }


        private void LimitLateralVelocity()
        {
            var lateralVelocity = Vector3.ProjectOnPlane(velocity, transform.up);
            if (lateralVelocity.sqrMagnitude > _baseFields.maxLateralSpeed * _baseFields.maxLateralSpeed)
                cachedRigidbody.velocity += lateralVelocity.normalized * _baseFields.maxLateralSpeed - lateralVelocity;
        }


        private void LimitVerticalVelocity()
        {
            if (isGrounded)
                return;

            var up = transform.up;

            var verticalSpeed = Vector3.Dot(velocity, up);
            if (verticalSpeed < -_baseFields.maxFallSpeed)
                cachedRigidbody.velocity += up * (-_baseFields.maxFallSpeed - verticalSpeed);
            if (verticalSpeed > _baseFields.maxRiseSpeed)
                cachedRigidbody.velocity += up * (_baseFields.maxRiseSpeed - verticalSpeed);
        }


        public void Move(Vector3 desiredVelocity, float maxDesiredSpeed, bool onlyLateral = true)
        {
            DetectGround();


            ApplyMovement(desiredVelocity, maxDesiredSpeed, onlyLateral);


            if (_baseFields.snapToGround && isOnGround)
                SnapToGround();


            LimitLateralVelocity();
            LimitVerticalVelocity();


            PreventGroundPenetration();
        }


        public void Move(Vector3 desiredVelocity, float maxDesiredSpeed, float acceleration, float deceleration,
            float friction, float brakingFriction, bool onlyLateral = true)
        {
            DetectGround();


            ApplyMovement(desiredVelocity, maxDesiredSpeed, acceleration, deceleration, friction, brakingFriction,
                onlyLateral);


            if (_baseFields.snapToGround && isGrounded)
                SnapToGround();


            LimitLateralVelocity();
            LimitVerticalVelocity();


            PreventGroundPenetration();
        }


        private void SnapToGround()
        {
            if (groundDistance < 0.001f)
                return;


            var otherRigidbody = groundRigidbody;
            if (otherRigidbody != null && otherRigidbody.isKinematic)
                return;


            const float groundOffset = 0.01f;

            var distanceToGround = Mathf.Max(0.0f, groundDistance - groundOffset);


            if (isOnLedgeSolidSide)
                distanceToGround = Mathf.Max(0.0f,
                    Vector3.Dot(transform.position - groundPoint, transform.up) - groundOffset);


            var snapVelocity = transform.up * (-distanceToGround * _baseFields.snapStrength / Time.deltaTime);

            var newVelocity = velocity + snapVelocity;

            velocity = newVelocity.normalized * velocity.magnitude;
        }


        private void SnapToPlatform(ref Vector3 probingPosition, ref Quaternion probingRotation)
        {
            if (_performGroundDetection == false || _forceUnground || _forceUngroundTimer > 0.0f)
                return;


            GroundHit hitInfo;
            if (!ComputeGroundHit(probingPosition, probingRotation, out hitInfo, pGroundDetection.castDistance))
                return;


            var otherRigidbody = hitInfo.groundRigidbody;
            if (otherRigidbody == null || !otherRigidbody.isKinematic)
                return;


            var up = probingRotation * Vector3.up;
            var groundedPosition = probingPosition - up * hitInfo.groundDistance;


            var pointVelocity = otherRigidbody.GetPointVelocity(groundedPosition);
            cachedRigidbody.velocity = velocity + pointVelocity;

            var deltaVelocity = pointVelocity - platformVelocity;
            groundedPosition += Vector3.ProjectOnPlane(deltaVelocity, up) * Time.deltaTime;


            if (hitInfo.isOnLedgeSolidSide)
                groundedPosition = MathLibrary.ProjectPointOnPlane(groundedPosition, hitInfo.groundPoint, up);


            probingPosition = groundedPosition;


            if (platformUpdatesRotation == false || otherRigidbody.angularVelocity == Vector3.zero)
                return;

            var yaw = Vector3.Project(otherRigidbody.angularVelocity, up);
            var yawRotation = Quaternion.Euler(yaw * (Mathf.Rad2Deg * Time.deltaTime));

            probingRotation *= yawRotation;
        }

        public void MovementFixedUpdate()
        {
            var p = transform.position;
            var q = transform.rotation;

            OverlapRecovery(ref p, q);


            if (isOnGround && isOnPlatform)
                SnapToPlatform(ref p, ref q);


            cachedRigidbody.MovePosition(p);
            cachedRigidbody.MoveRotation(q);
        }

        #endregion

        #region MONOBEHAVIOUR
        
        public virtual void LateUpdate()
        {}

        public virtual void FixedUpdate()
        {
            if (isPaused)
                return;

            MovementFixedUpdate();
            Move();
            Crouch();
        }

        public virtual void Update()
        {
            if (isPaused)
                return;

            UpdateRotation();
        }

        #endregion
    }
}