using TimeSystem;
using UnityEngine;

namespace TimeMovementSystem
{
    public class MoveForwardComponent : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.1f;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            _transform.position += _transform.forward * TimeManagerComponent.TimeManager.ScaledTimeSpeed() * _speed;
        }
    }
}