using Entitas;
using Entitas.CodeGeneration.Attributes;
using TimelineData;

[Time, Unique]
public class TimelineRewindPositionComponent : IComponent
{
    public PlayerTimelineData Value;
}