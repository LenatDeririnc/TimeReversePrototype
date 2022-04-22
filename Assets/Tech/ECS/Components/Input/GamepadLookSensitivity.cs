using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique, Input]
public class GamepadLookSensitivity : IComponent
{
    public float Value;
}