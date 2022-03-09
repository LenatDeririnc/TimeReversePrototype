using System;
using Common;
using SingletonSystem;
using UnityEngine;

namespace InputHandler
{
    public class InputHandlerComponent : Singleton<InputHandlerComponent>
    {
        public bool run { get; private set; }
        public bool pause { get; private set; }

        public Vector3 moveDirection;

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

            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            // Handle user input

            var move = new Vector3()
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = 0.0f,
                z = Input.GetAxisRaw("Vertical")
            };
            Vector3.ClampMagnitude(move, 1);

            moveDirection = move;

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