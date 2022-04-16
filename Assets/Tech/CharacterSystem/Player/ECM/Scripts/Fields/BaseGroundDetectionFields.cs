using System;
using UnityEngine;

namespace CharacterSystem.Player.ECM.Scripts.Fields
{
    [Serializable]
    public class BaseGroundDetectionFields
    {
        public LayerMask _groundMask = 1;
        public float _groundLimit = 60.0f;
        public float _stepOffset = 0.5f;
        public float _ledgeOffset;
        public float _castDistance = 0.5f;
        public QueryTriggerInteraction _triggerInteraction = QueryTriggerInteraction.Ignore;
    }
}