using TimeSystem;
using UnityEngine;

namespace CharacterSystem
{
    public class EnemyComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private static readonly int Direction = Animator.StringToHash("Direction");

        void Update()
        {
            _animator.SetFloat(Direction, TimeManager.TimeHandler.timeSpeed);
        }
    }
}
