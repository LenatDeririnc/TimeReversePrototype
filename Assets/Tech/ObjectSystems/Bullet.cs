using ECS.Mono;
using UnityEngine;

namespace ObjectSystems
{
    public class Bullet : MonoProvider
    {
        private void OnTriggerEnter(Collider other)
        {
            var signal = Contexts.game.CreateEntity();
            signal.isShot = true;
            signal.ReplaceTriggerSignal(other);
        }
    }
}