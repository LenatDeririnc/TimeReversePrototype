using Common;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Time, Unique]
public class TimelineLastPositionComponent : IComponent
{
    public TimelineData Value;
}