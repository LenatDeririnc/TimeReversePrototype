using System;
using CharacterSystem;
using CompassSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace TimeSystem
{
    public class TimeChangerInjector : MonoBehaviour
    {
        [FormerlySerializedAs("timeChanger")] [SerializeField] private MonoBehaviour movingCharacter;
        [SerializeField] private CompassComponent _compassComponent;

        private void Start()
        {
            TimeManagerComponent.TimeManager.SetMovingObject(PlayerComponent.Instance.Velocity);
        }
    }
}