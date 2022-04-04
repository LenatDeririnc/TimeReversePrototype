using Common;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Time, Unique]
public class TimelineRewindPositionComponent : IComponent
{
    public TimelineData Value;
}