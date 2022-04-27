using System;
using ECS.Tools;
using UnityEngine;

namespace ECS
{
    public class EcsBootstrapper : MonoBehaviour
    {
        public EcsManagerType SelectedEcsType;
        public static Contexts Contexts;
        public BaseEcsManager EcsManager;
        private GameObjectEntityTools _gameObjectEntityTools;

        private void Awake()
        {
            Contexts ??= new Contexts();

            _gameObjectEntityTools = new GameObjectEntityTools(Contexts);

            EcsManager = SelectedEcsType switch
            {
                EcsManagerType.GameEcsManager => new GameEcsManager(Contexts),
                EcsManagerType.TestUnitEcsManager => new TestUnitEcsManager(Contexts),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void Start()
        {
            EcsManager.Systems.Initialize();
        }

        public void LateUpdate()
        {
            EcsManager.LateSystems.Execute();
        }
        
        public void FixedUpdate()
        {
            EcsManager.FixedSystems.Execute();
        }

        public void Update()
        {
            EcsManager.Systems.Execute();
        }

        public void OnDestroy()
        {
            EcsManager.Systems.TearDown();
        }
    }
}