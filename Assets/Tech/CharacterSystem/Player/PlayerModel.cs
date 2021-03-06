using System;
using CharacterSystem.Player.ECM.Scripts.Fields;
using UnityEngine;

namespace CharacterSystem.Player
{
    public class PlayerModel : MonoBehaviour
    {
        public InputEntity inputEntity;
        public GameEntity playerEntity;
        public GameEntity cameraEntity;
        public TimeEntity timeMovementEntity;
        public TimeEntity timeFireEntity;
    
        public Transform transform;
        public GameObject gameObject;
        public Rigidbody rigidBody;
        public CapsuleCollider capsuleCollider;
        public Animator Animator;
        public Camera Camera;
        public Transform CameraTransform;
        public Transform CameraPivotTransform;
        
        public MouseLookFields MouseLookFields;
        public BaseGroundDetectionFields BaseGroundDetectionFields;
        public BasePlayerControllerFields BasePlayerControllerFields;
        public BasePlayerFirstPersonControllerFields BasePlayerFirstPersonControllerFields;
        public PlayerController Controller;
    }
}