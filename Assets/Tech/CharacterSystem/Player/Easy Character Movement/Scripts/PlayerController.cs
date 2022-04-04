using Common;
using DamageSystem;
using ECM.Components;
using ECM.Controllers;
using ECM.Fields;
using ECM.Helpers;
using ECS.Mono;
using UnityEngine;

namespace ECM
{
    public class PlayerController : MonoProvider, IInputControlling, IDestroyable
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
            _playerEntity.ReplaceTransform(PlayerModel.transform);
            _playerEntity.ReplaceTransformInfo(new TransformInfo(PlayerModel.transform));

            _cameraEntity = Contexts.game.CreateEntity();
            _cameraEntity.ReplacePlayerCamera(PlayerModel.Camera);
            _cameraEntity.ReplaceTransform(PlayerModel.CameraTransform);
            _cameraEntity.ReplaceCameraPitchAngle(PlayerModel.CameraTransform.rotation.eulerAngles.x);
        }
        
        private void OnDrawGizmosSelected()
        {
            BaseGroundDetection?.OnDrawGizmosSelected();
        }

        public void SendInputData(InputData data)
        {
            BasePlayerController.HandleInput(data);
            MouseLook.HandleInput(data);
        }

        public void Destroy()
        {
            Debug.Log("YOU DEAD");
        }
    }
}