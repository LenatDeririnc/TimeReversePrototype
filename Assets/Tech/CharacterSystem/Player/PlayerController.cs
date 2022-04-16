using System;
using CharacterSystem.Player.ECM.Scripts.Components;
using CharacterSystem.Player.ECM.Scripts.Components.GroundDetection;
using CharacterSystem.Player.ECM.Scripts.Controllers;
using CharacterSystem.Player.ECM.Scripts.Fields;
using CharacterSystem.Player.ECM.Scripts.Helpers;
using Common;
using ECS.Mono;
using UnityEngine;

namespace CharacterSystem.Player
{
    public class PlayerController : MonoProvider, IInputControlling
    {
        private InputEntity _inputEntity;
        private GameEntity _playerEntity;
        private GameEntity _cameraEntity;

        public PlayerModel PlayerModel;
        public BasePlayerController BasePlayerController;
        public MouseLook MouseLook;
        public BaseGroundDetection BaseGroundDetection;
        public RootMotionController RootMotionController;

        private void OnValidate()
        {
            if (PlayerModel == null)
            {
                Debug.Log("Set Player Model");
                return;
            }
        }

        protected void Awake()
        {
            BaseGroundDetection = new GroundDetection(PlayerModel);
            MouseLook = new MouseLook(PlayerModel);
            BasePlayerController = new BaseFirstPersonController(Contexts, PlayerModel, this);

            if (PlayerModel.Animator != null)
                RootMotionController = new RootMotionController(PlayerModel.Animator);
            
            _inputEntity = Contexts.input.CreateEntity();
            _inputEntity.ReplaceInputControlling(this);
            _inputEntity.ReplaceBasePlayerControllerHolder(BasePlayerController);

            _playerEntity = Contexts.game.CreateEntity();
            _playerEntity.isPlayer = true;
            _playerEntity.isShot = false;
            _playerEntity.ReplaceTransform(PlayerModel.transform);
            _playerEntity.ReplaceTransformInfo(new TransformInfo(PlayerModel.transform));

            _cameraEntity = Contexts.game.CreateEntity();
            _cameraEntity.ReplacePlayerCamera(PlayerModel.Camera);
            _cameraEntity.ReplaceTransform(PlayerModel.CameraTransform);
            _cameraEntity.ReplaceCameraPitchAngle(PlayerModel.CameraTransform.rotation.eulerAngles.x);
        }

        private void Start()
        {
            Contexts.game.CreateEntity().ReplaceAddColliderDataSignal(PlayerModel.capsuleCollider, _playerEntity);
        }

        private void OnDrawGizmosSelected()
        {
            BaseGroundDetection?.OnDrawGizmosSelected();
        }

        public void SendInputData(InputData data)
        {
            if (_playerEntity.isShot)
                data = new InputData();
                
            BasePlayerController.HandleInput(data);
            MouseLook.HandleInput(data);
        }
    }
}