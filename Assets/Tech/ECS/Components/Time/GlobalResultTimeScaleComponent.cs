using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique, Time]
public class GlobalResultTimeScaleComponent : IComponent
{
    public float Value;
}