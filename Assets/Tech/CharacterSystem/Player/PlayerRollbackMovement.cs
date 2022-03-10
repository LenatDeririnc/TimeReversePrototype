using Common;
using TimeSystem;
using UnityEngine;

namespace CharacterSystem.Player
{
    public class PlayerRollbackMovement
    {
        private readonly Transform _transform;
        private TransformInfo _lastPosition;
        private TransformInfo _newData;
        private bool _isPositionUpdated = false;

        public PlayerRollbackMovement(Transform transform)
        {
            _transform = transform;
        }

        public void Update()
        {
            if (!RollbackController.Instance.IsRollbackActive())
            {
                _isPositionUpdated = false;
                return;
            }

            if (!_isPositionUpdated)
                return;

            var tm = TimeManagerComponent.TimeManager;
            _transform.position = Vector3.Lerp(_lastPosition.position, _newData.position, 1-tm.TickRateDivideRatio);
            _transform.rotation = Quaternion.Lerp(_lastPosition.rotation, _newData.rotation, 1-tm.TickRateDivideRatio);
        }

        public void SetPosition(TransformInfo data)
        {
            _newData = data;
            _lastPosition = new TransformInfo()
            {
                position = _transform.position,
                rotation = _transform.rotation,
            };
            _isPositionUpdated = true;
        }
    }
}