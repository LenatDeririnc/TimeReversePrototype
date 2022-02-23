using UnityEngine;

namespace TimeSystem
{
    public class TimeChangerInjector : MonoBehaviour
    {
        private void Awake()
        {
            var timeChanger = GetComponent<ITimeChanger>();

            if (timeChanger == null)
                return;

            TimeManager.TimeHandler = new TimeHandler(timeChanger);
        }
    }
}