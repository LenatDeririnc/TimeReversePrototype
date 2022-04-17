using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique, Time]
public class PreviousTimeComponent : IComponent
{
    public float Value;
}