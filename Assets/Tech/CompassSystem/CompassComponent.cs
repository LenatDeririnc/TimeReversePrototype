using Helpers;
using SingletonSystem;
using UnityEngine;

namespace CompassSystem
{
    public class CompassComponent : Singleton<CompassComponent>
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Sector _rollbackSector;
        public Transform Transform { get; private set; }
        public Sector RollbackSector => _rollbackSector;


        protected override void Awake()
        {
            base.Awake();
            Transform = transform;
            _rollbackSector.transform = Transform;
        }

        private void Start()
        {
            UpdateForward();
        }

        public void UpdateForward()
        {
            Transform.position = _playerTransform.position;
            transform.rotation = _playerTransform.rotation;
        }

        private void Update()
        {
            UpdateForward();
        }
    }


}