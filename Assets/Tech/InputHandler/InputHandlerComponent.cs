using System;
using CharacterSystem;
using Common;
using CompassSystem;
using SingletonSystem;
using UnityEngine;

namespace InputHandler
{
    public class InputHandlerComponent : Singleton<InputHandlerComponent>
    {
        public bool run { get; private set; }
        public bool pause { get; private set; }

        public Vector3 moveDirection { get; private set; }
        public VelocityConverter moveDirectionVelocity { get; } = new VelocityConverter(Vector3.zero);

        public VelocityConverter ForwardMovement { get; } = new VelocityConverter(Vector3.zero);

        public VelocityConverter BackMovement  { get; } = new VelocityConverter(Vector3.zero);

        public bool jump { get; private set; }
        public bool crouch { get; private set; }
        public bool isPaused { get; private set; }

        public float mouseX { get; private set; }
        public float mouseY { get; private set; }

        public static Action<bool> OnPauseChanged;


        private void Pause()
        {
            var newIsPaused = pause switch
            {
                true when !isPaused => true,
                false when isPaused => false,
                _ => isPaused
            };

            if (newIsPaused == isPaused)
                return;

            isPaused = newIsPaused;
            OnPauseChanged?.Invoke(isPaused);
        }

        public virtual void HandleInput()
        {
            // Toggle pause / resume.
            // By default, will restore character's velocity on resume (eg: restoreVelocityOnResume = true)

            if (Input.GetKeyDown(KeyCode.P))
                pause = !pause;

            mouseX = Mathf.Clamp(Input.GetAxis("Mouse X") + Input.GetAxis("HorizontalLook"), -1, 1);
            mouseY = Mathf.Clamp(Input.GetAxis("Mouse Y") + Input.GetAxis("VerticalLook"), -1, 1);

            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            // Handle user input

            var move = new Vector3()
            {
                x = horizontal,
                y = 0.0f,
                z = vertical
            };
            Vector3.ClampMagnitude(move, 1);

            moveDirection = move;
            moveDirectionVelocity.Set(move);

            var forward = new Vector3(horizontal, 0, Mathf.Clamp(vertical, 0, 1));

            ForwardMovement.Set(forward);

            BackMovement.Set(moveDirection - forward);

            run = Input.GetButton("Fire3");

            jump = Input.GetButton("Jump");

            crouch = Input.GetKey(KeyCode.C);
        }

        private void FixedUpdate()
        {
            Pause();
        }

        private void Update()
        {
            HandleInput();
        }
    }
}