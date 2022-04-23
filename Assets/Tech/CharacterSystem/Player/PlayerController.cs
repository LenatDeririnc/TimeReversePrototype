using CharacterSystem.Player.ECM.Scripts.Components.GroundDetection;
using CharacterSystem.Player.ECM.Scripts.Controllers;
using CharacterSystem.Player.ECM.Scripts.Fields;
using CharacterSystem.Player.ECM.Scripts.Helpers;
using Common;
using ECS.Mono;
using UnityEngine;
using MouseLook = CharacterSystem.Player.ECM.Scripts.Components.MouseLook;

namespace CharacterSystem.Player
{
    public class PlayerController : MonoProvider, IInputControlling
    {
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
                
            Contexts.game.SetPlayerModel(PlayerModel);
            
            PlayerModel.inputEntity = Contexts.input.CreateEntity();
            PlayerModel.inputEntity.ReplaceInputControlling(this);
            PlayerModel.inputEntity.ReplaceBasePlayerControllerHolder(BasePlayerController);

            PlayerModel.playerEntity = Contexts.game.CreateEntity();
            PlayerModel.playerEntity.isPlayer = true;
            PlayerModel.playerEntity.ReplaceTransform(PlayerModel.transform);
            PlayerModel.playerEntity.ReplaceTransformInfo(new TransformInfo(PlayerModel.transform));

            PlayerModel.cameraEntity = Contexts.game.CreateEntity();
            PlayerModel.cameraEntity.ReplacePlayerCamera(PlayerModel.Camera);
            PlayerModel.cameraEntity.ReplaceTransform(PlayerModel.CameraTransform);
            PlayerModel.cameraEntity.ReplaceCameraPitchAngle(PlayerModel.CameraTransform.rotation.eulerAngles.x);
            
            PlayerModel.timeEntity = Contexts.time.CreateEntity();
            PlayerModel.timeEntity.ReplaceTimeSpeed(0);
        }

        private void Start()
        {
            Contexts.game.CreateEntity().ReplaceAddColliderDataSignal(PlayerModel.capsuleCollider, PlayerModel.playerEntity);
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
    }
}