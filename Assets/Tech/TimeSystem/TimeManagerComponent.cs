using ECS;
using InputHandler;
using UnityEngine;

namespace TimeSystem
{
    public class TimeManagerComponent : MonoBehaviour
    {
        public static TimeManager TimeManager
            => EcsBootstrapper.Contexts.time.timeManagerHandlerEntity.timeManagerHandler.Value;
        
        // private void Update()
        // {
        //     TimeManager.Update();
        // }
    }
}