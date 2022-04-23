using CharacterSystem.Player.ECM.Scripts.Components.GroundDetection;
using CharacterSystem.Player.ECM.Scripts.Controllers;
using CharacterSystem.Player.ECM.Scripts.Fields;
using CharacterSystem.Player.ECM.Scripts.Helpers;
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