﻿using Entitas;
using UnityEngine;

namespace ECS.Systems.Input
{
    public class MouseLookInputSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _group;

        public MouseLookInputSystem(Contexts contexts)
        {
            _group = contexts.input.GetGroup(InputMatcher.Input);
        }

        public void Execute()
        {
            foreach (var e in _group)
            {
                var mouseX = UnityEngine.Input.GetAxis("Mouse X");
                var mouseY = UnityEngine.Input.GetAxis("Mouse Y");

                e.look.Value = new Vector2(mouseX, mouseY);
            }
        }
    }
}