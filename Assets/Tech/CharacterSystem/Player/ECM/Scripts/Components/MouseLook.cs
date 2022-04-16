using CharacterSystem.Player.ECM.Scripts.Controllers;
using CharacterSystem.Player.ECM.Scripts.Fields;
using UnityEngine;

namespace CharacterSystem.Player.ECM.Scripts.Components
{
    public class MouseLook
    {
        private MouseLookFields _fields;
        public MouseLook(PlayerModel model)
        {
            characterTargetRotation = model.transform.localRotation;
            cameraTargetRotation = model.Camera.transform.localRotation;
            _fields = model.MouseLookFields;
        }

        private Vector3 _lookVelocity = new Vector3(0, 0, 0);


        protected bool _isCursorLocked = true;

        protected Quaternion characterTargetRotation;
        protected Quaternion cameraTargetRotation;
        private float _yaw;
        private float _pitch;


        #region PROPERTIES

        public bool lockCursor
        {
            get => _fields._lockCursor;
            set => _fields._lockCursor = value;
        }


        public KeyCode unlockCursorKey
        {
            get => _fields._unlockCursorKey;
            set => _fields._unlockCursorKey = value;
        }


        public float lateralSensitivity
        {
            get => _fields._lateralSensitivity;
            set => _fields._lateralSensitivity = Mathf.Max(0.0f, value);
        }


        public float verticalSensitivity
        {
            get => _fields._verticalSensitivity;
            set => _fields._verticalSensitivity = Mathf.Max(0.0f, value);
        }


        public bool smooth
        {
            get => _fields._smooth;
            set => _fields._smooth = value;
        }


        public float smoothTime
        {
            get => _fields._smoothTime;
            set => _fields._smoothTime = Mathf.Max(0.0f, value);
        }


        public bool clampPitch
        {
            get => _fields._clampPitch;
            set => _fields._clampPitch = value;
        }


        public float minPitchAngle
        {
            get => _fields._minPitchAngle;
            set => _fields._minPitchAngle = Mathf.Clamp(value, -180.0f, 180.0f);
        }


        public float maxPitchAngle
        {
            get => _fields._maxPitchAngle;
            set => _fields._maxPitchAngle = Mathf.Clamp(value, -180.0f, 180.0f);
        }

        #endregion

        #region METHODS

        public virtual void LookRotation(IPlayerMovementForMouse movement, GameEntity cameraEntity)
        {
            _lookVelocity = new Vector3(_pitch, _yaw, 0);
            cameraEntity.cameraPitchAngle.Value = _pitch;

            var yawRotation = Quaternion.Euler(0.0f, _yaw, 0.0f);
            var pitchRotation = Quaternion.Euler(-_pitch, 0.0f, 0.0f);

            characterTargetRotation *= yawRotation;
            cameraTargetRotation *= pitchRotation;

            if (clampPitch)
                cameraTargetRotation = ClampPitch(cameraTargetRotation);

            if (smooth)
            {
                if (movement.platformUpdatesRotation && movement.isOnPlatform &&
                    movement.platformAngularVelocity != Vector3.zero)
                    characterTargetRotation *=
                        Quaternion.Euler(movement.platformAngularVelocity * Mathf.Rad2Deg * Time);

                movement.rotation = Quaternion.Slerp(movement.rotation, characterTargetRotation,
                    smoothTime * Time);

                cameraEntity.transform.Value.localRotation = Quaternion.Slerp(cameraEntity.transform.Value.localRotation, cameraTargetRotation,
                    smoothTime * Time);
            }
            else
            {
                movement.rotation *= yawRotation;
                cameraEntity.transform.Value.localRotation *= pitchRotation;

                if (clampPitch)
                    cameraEntity.transform.Value.localRotation = ClampPitch(cameraEntity.transform.Value.localRotation);
            }
            
            cameraEntity.cameraPitchAngle.Value = cameraEntity.transform.Value.localRotation.eulerAngles.x;

            UpdateCursorLock();
        }

        public float Time => 0.001f;

        public virtual void SetCursorLock(bool value)
        {
            lockCursor = value;
            if (lockCursor)
                return;


            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public virtual void UpdateCursorLock()
        {
            if (lockCursor)
                InternalLockUpdate();
        }

        protected virtual void InternalLockUpdate()
        {
            if (Input.GetKeyUp(unlockCursorKey))
                _isCursorLocked = false;
            else if (Input.GetMouseButtonUp(0))
                _isCursorLocked = true;

            if (_isCursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (!_isCursorLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        protected Quaternion ClampPitch(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            var pitch = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            pitch = Mathf.Clamp(pitch, minPitchAngle, maxPitchAngle);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * pitch);

            return q;
        }

        public void HandleInput(InputData data)
        {
            _yaw = data.look.x * lateralSensitivity;
            _pitch = data.look.y * verticalSensitivity;
        }

        #endregion
    }
}