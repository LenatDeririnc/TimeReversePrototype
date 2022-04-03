using UnityEngine;

namespace ECS.Mono
{
    public class MonoProvider : MonoBehaviour
    {
        protected Contexts Contexts;

        protected virtual void Awake()
        {
            Contexts = Contexts.sharedInstance;
        }
    }
}