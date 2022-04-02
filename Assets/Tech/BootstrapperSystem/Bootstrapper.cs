using System;
using ECS;
using UnityEngine;

namespace BootstrapperSystem
{
    public class Bootstrapper : MonoBehaviour
    {
        private EcsBootstrapper _ecsBootstrapper;

        private void Awake()
        {
            _ecsBootstrapper = new EcsBootstrapper();
        }

        private void OnEnable()
        {
            _ecsBootstrapper?.Initialize();
        }

        private void Update()
        {
            _ecsBootstrapper?.Execute();
        }

        private void OnDisable()
        {
            _ecsBootstrapper?.TearDown();
        }
    }
}