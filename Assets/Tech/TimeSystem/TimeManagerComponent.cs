using UnityEngine;

namespace TimeSystem
{
    public class TimeManagerComponent : MonoBehaviour
    {
        public static TimeManager TimeManager = new TimeManager();

        private void Update()
        {
            TimeManager.Update();
        }
    }
}