using Common;
using Entitas;
using UnityEngine;

namespace ECS.Systems.Characters.Player
{
    public class InitPlayerReactiveSystem : IInitializeSystem
    {
        private readonly Contexts _contexts;

        public InitPlayerReactiveSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void Initialize()
        {
            var playerModel = _contexts.game.playerModel.Value;
            
            playerModel.inputEntity = _contexts.input.CreateEntity();
            playerModel.inputEntity.ReplaceInputControlling(playerModel.Controller);
            playerModel.inputEntity.ReplaceBasePlayerControllerHolder(playerModel.Controller.BasePlayerController);

            playerModel.playerEntity = _contexts.game.CreateEntity();
            EcsManager.GameObjectEntityTools.SetEntityUniqueId(playerModel.playerEntity);
            playerModel.playerEntity.isPlayer = true;
            playerModel.playerEntity.ReplaceTransform(playerModel.transform);
            playerModel.playerEntity.ReplaceTransformInfo(new TransformInfo(playerModel.transform));
            playerModel.playerEntity.ReplacePlayerRigidBody(playerModel.rigidBody);
            EcsManager.GameObjectEntityTools.AddColliderData(playerModel.capsuleCollider, playerModel.playerEntity);

            playerModel.cameraEntity = _contexts.game.CreateEntity();
            EcsManager.GameObjectEntityTools.SetEntityUniqueId(playerModel.cameraEntity);
            playerModel.cameraEntity.ReplacePlayerCamera(playerModel.Camera);
            playerModel.cameraEntity.ReplaceTransform(playerModel.CameraTransform);
            playerModel.cameraEntity.ReplaceCameraPitchAngle(playerModel.CameraTransform.rotation.eulerAngles.x);
            
            playerModel.timeMovementEntity = _contexts.time.CreateEntity();
            playerModel.timeMovementEntity.ReplaceTimeSpeed(0);
            playerModel.timeFireEntity = _contexts.time.CreateEntity();
            playerModel.timeFireEntity.ReplaceTimeSpeed(0);
        }
    }
}