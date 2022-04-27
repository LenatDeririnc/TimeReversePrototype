using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class CameraPitchAngleComponent : IComponent
{
    public float Value;
}