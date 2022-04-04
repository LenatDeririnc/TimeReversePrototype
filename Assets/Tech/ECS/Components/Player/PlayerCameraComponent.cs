using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique]
public class PlayerCameraComponent : IComponent
{
    public Camera Value;
}