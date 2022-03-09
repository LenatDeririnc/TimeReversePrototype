using CompassSystem;
using EditorTools;
using InputHandler;
using UnityEngine;
using UnityEngine.Serialization;

namespace TimeSystem
{
    public class TimeChangerInjector : DeprecatedComponent
    {
        [FormerlySerializedAs("timeChanger")] [SerializeField] private MonoBehaviour movingCharacter;
        [SerializeField] private CompassComponent _compassComponent;

        private void Start()
        {
            TimeManagerComponent.TimeManager.SetMovingObject(InputHandlerComponent.Instance.moveDirectionVelocity);
        }
    }
}