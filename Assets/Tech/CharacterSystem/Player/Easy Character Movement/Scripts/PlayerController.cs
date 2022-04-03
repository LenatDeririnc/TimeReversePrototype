using ECM.Components;
using ECM.Controllers;
using ECM.Fields;
using ECM.Helpers;
using ECS.Mono;
using UnityEngine;

namespace ECM
{
    public class PlayerController : MonoProvider, IInputControlling
    {
        private InputEntity _inputEntity;
        
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

        protected override void Awake()
        {
            base.Awake();
            _inputEntity = Contexts.input.CreateEntity();
            _inputEntity.ReplaceInputControlling(this);
        }

        private void LateUpdate()
        {
            BasePlayerController.LateUpdate();
        }

        private void FixedUpdate()
        {
            BasePlayerController.FixedUpdate();
        }

        private void Update()
        {
            BasePlayerController.Update();
        }

        private void OnDrawGizmosSelected()
        {
            BaseGroundDetection?.OnDrawGizmosSelected();
        }

        public void SendInputData(InputData data)
        {
            Debug.Log(data.ToString());
            BasePlayerController.HandleInput(data);
            MouseLook.HandleInput(data);
        }
    }
}