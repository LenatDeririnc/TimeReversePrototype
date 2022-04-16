using System;

namespace CharacterSystem.Player.ECM.Scripts.Fields
{
    [Serializable]
    public class BasePlayerFirstPersonControllerFields
    {
        public float _forwardSpeed = 5.0f;
        public  float _backwardSpeed = 3.0f;
        public  float _strafeSpeed = 4.0f;
        public  float _runSpeedMultiplier = 2.0f;
    }
}