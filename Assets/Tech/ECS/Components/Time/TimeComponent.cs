using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique, Time]
public class TimeComponent : IComponent
{
    public float Value;
}