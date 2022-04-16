using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique, Time]
public class TickRateComponent : IComponent
{
    public float Value;
}