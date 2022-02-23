using System;
using UnityEngine;

namespace TimeSystem
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeHandler TimeHandler;

        private void Update()
        {
            TimeHandler?.UpdateTime();
        }
    }
}