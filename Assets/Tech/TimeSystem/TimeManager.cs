using UnityEngine;

namespace TimeSystem
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeHandler TimeHandler = new TimeHandler();

        private void Update()
        {
            TimeHandler?.Update();
        }
    }
}