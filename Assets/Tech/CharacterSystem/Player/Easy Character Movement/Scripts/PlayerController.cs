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
        private GameEntity _gameEntity;

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
            
            BaseGroundDetection = new GroundDetection(PlayerModel);
            MouseLook = new MouseLook(PlayerModel);
            BasePlayerController = new BaseFirstPersonController(PlayerModel, this);

            if (PlayerModel.Animator != null)
                RootMotionController = new RootMotionController(PlayerModel.Animator);
        }

        protected void Awake()
        {
            _inputEntity = Contexts.input.CreateEntity();
            _inputEntity.ReplaceInputControlling(this);
            _inputEntity.ReplaceBasePlayerControllerHolder(BasePlayerController);

            _gameEntity = Contexts.game.CreateEntity();
            _gameEntity.isPlayer = true;
            _gameEntity.ReplaceTransform(PlayerModel.transform);
            _gameEntity.ReplaceTransformInfo(new TransformInfo(PlayerModel.transform));
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