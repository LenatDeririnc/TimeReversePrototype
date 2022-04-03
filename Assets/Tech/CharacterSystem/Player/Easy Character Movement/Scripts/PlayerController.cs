using System;
using ECM.Components;
using ECM.Controllers;
using ECM.Helpers;
using UnityEngine;

namespace ECM
{
    public class PlayerController : MonoBehaviour
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
            
            BaseGroundDetection = new GroundDetection(PlayerModel);
            MouseLook = new MouseLook(PlayerModel);
            BasePlayerController = new BaseFirstPersonController(PlayerModel, this);

            if (PlayerModel.Animator != null)
                RootMotionController = new RootMotionController(PlayerModel.Animator);
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
    }
}