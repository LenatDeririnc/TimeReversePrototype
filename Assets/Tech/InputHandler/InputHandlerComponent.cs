using System;
using Common;
using SingletonSystem;
using UnityEngine;

namespace InputHandler
{
    public class InputHandlerComponent : Singleton<InputHandlerComponent>
    {
        public bool run { get; set; }
        public bool pause { get; set; }

        public MoveDirection moveDirection;

        public bool jump { get; set; }
        public bool crouch { get; set; }
        public bool isPaused { get; set; }

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

            // Handle user input

            var move = new Vector3()
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = 0.0f,
                z = Input.GetAxisRaw("Vertical")
            };
            Vector3.ClampMagnitude(move, 1);

            moveDirection = new MoveDirection(move);

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

    public readonly struct MoveDirection : IVelocity
    {
        private readonly Vector3 _value;

        public MoveDirection(Vector3 newDirection)
        {
            _value = newDirection;
        }

        public Vector3 Velocity() =>
            _value;
    }
}