using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique, Input]
public class MouseLookSensitivity : IComponent
{
    public float Value;
}