using ECM.Components;
using ECM.Fields;
using UnityEngine;

namespace ECM.Controllers
{
    public class BaseFirstPersonController : BasePlayerController
    {
        private BasePlayerFirstPersonControllerFields _fields;
        
        public BaseFirstPersonController(PlayerModel model, PlayerController playerController) : base(model, playerController)
        {
            _fields = model.BasePlayerFirstPersonControllerFields;
            
            if (cameraPivotTransform == null)
                Debug.LogError(string.Format(
                    "BaseFPSController: No 'Camera_Pivot' found. Please parent a transform gameobject to '{0}' game object.",
                    model.transform.name));
        }

        #region PROPERTIES

        public Transform cameraPivotTransform { get => _model.CameraPivotTransform;}


        public Transform cameraTransform { get => _model.CameraTransform; }


        public MouseLook mouseLook { get => _playerController.MouseLook; }


        public float forwardSpeed
        {
            get => _fields._forwardSpeed;
            set => _fields._forwardSpeed = Mathf.Max(0.0f, value);
        }


        public float backwardSpeed
        {
            get => _fields._backwardSpeed;
            set => _fields._backwardSpeed = Mathf.Max(0.0f, value);
        }


        public float strafeSpeed
        {
            get => _fields._strafeSpeed;
            set => _fields._strafeSpeed = Mathf.Max(0.0f, value);
        }


        public float runSpeedMultiplier
        {
            get => _fields._runSpeedMultiplier;
            set => _fields._runSpeedMultiplier = Mathf.Max(value, 1.0f);
        }


        public bool run { get; set; }

        #endregion

        #region METHODS

        protected virtual void AnimateView()
        {
            var yScale = isCrouching ? Mathf.Clamp01(_baseFields.crouchingHeight / _baseFields.standingHeight) : 1.0f;

            cameraPivotTransform.localScale = Vector3.MoveTowards(cameraPivotTransform.localScale,
                new Vector3(1.0f, yScale, 1.0f), 5.0f * Time.deltaTime);
        }


        protected virtual void RotateView()
        {
            mouseLook.LookRotation(this, cameraTransform);
        }


        protected override void UpdateRotation()
        {
            RotateView();
        }


        protected virtual float GetTargetSpeed()
        {
            var targetSpeed = forwardSpeed;


            if (moveDirection.x > 0.0f || moveDirection.x < 0.0f)
                targetSpeed = strafeSpeed;


            if (moveDirection.z < 0.0f)
                targetSpeed = backwardSpeed;


            if (moveDirection.z > 0.0f)
                targetSpeed = forwardSpeed;


            return run ? targetSpeed * runSpeedMultiplier : targetSpeed;
        }


        protected override Vector3 CalcDesiredVelocity()
        {
            _baseFields.speed = GetTargetSpeed();


            return transform.TransformDirection(base.CalcDesiredVelocity());
        }

        #endregion

        #region MONOBEHAVIOUR

        public override void LateUpdate()
        {
            AnimateView();
        }

        #endregion
    }
}