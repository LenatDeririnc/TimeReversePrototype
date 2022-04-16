using Common;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique, Time]
public class TimeChangerComponent : IComponent
{
    public IVelocity Value;
}