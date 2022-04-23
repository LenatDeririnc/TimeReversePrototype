﻿using Common;
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
        
            //TODO: Перенести в отдельную систему, и сделать input уникальным
            // {
            playerModel.inputEntity = _contexts.input.CreateEntity();
            playerModel.inputEntity.isInput = true;
            playerModel.inputEntity.ReplaceLook(new Vector2(0, 0));
            playerModel.inputEntity.ReplaceMoveDirection(Vector3.zero);
            playerModel.inputEntity.ReplaceForwardMovement(Vector3.zero);
            playerModel.inputEntity.ReplaceBackMovement(Vector3.zero);
            playerModel.inputEntity.ReplaceInputControlling(playerModel.Controller);
            playerModel.inputEntity.ReplaceFireInput(0);
            playerModel.inputEntity.ReplaceBasePlayerControllerHolder(playerModel.Controller.BasePlayerController);
            // }

            playerModel.playerEntity = _contexts.game.CreateEntity();
            playerModel.playerEntity.isPlayer = true;
            playerModel.playerEntity.ReplaceTransform(playerModel.transform);
            playerModel.playerEntity.ReplaceTransformInfo(new TransformInfo(playerModel.transform));

            playerModel.cameraEntity = _contexts.game.CreateEntity();
            playerModel.cameraEntity.ReplacePlayerCamera(playerModel.Camera);
            playerModel.cameraEntity.ReplaceTransform(playerModel.CameraTransform);
            playerModel.cameraEntity.ReplaceCameraPitchAngle(playerModel.CameraTransform.rotation.eulerAngles.x);
            
            playerModel.timeEntity = _contexts.time.CreateEntity();
            playerModel.timeEntity.ReplaceTimeSpeed(0);
            
            _contexts.game.CreateEntity().ReplaceAddColliderDataSignal(playerModel.capsuleCollider, playerModel.playerEntity);
        }
    }
}