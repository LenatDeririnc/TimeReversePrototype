using UnityEngine;

namespace TimeSystem
{
    public class TimeChangerInjector : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour timeChanger;

        private void Awake()
        {
            var changer = timeChanger.GetComponent<ITimeChanger>();

            if (changer == null)
                return;
            
            TimeManager.TimeHandler.SetTimeChanger(changer);
        }
    }
}