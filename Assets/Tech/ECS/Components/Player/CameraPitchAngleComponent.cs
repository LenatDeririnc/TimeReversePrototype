using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique]
public class CameraPitchAngleComponent : IComponent
{
    public float Value;
}