using InputHandler;
using UnityEngine;

namespace TimeSystem
{
    public class TimeManagerComponent : MonoBehaviour
    {
        public static TimeManager TimeManager = new TimeManager();

        private void Start()
        {
            TimeManager.SetMovingObject(InputHandlerComponent.Instance.moveDirectionVelocity);
        }

        private void Update()
        {
            TimeManager.Update();
        }
    }
}