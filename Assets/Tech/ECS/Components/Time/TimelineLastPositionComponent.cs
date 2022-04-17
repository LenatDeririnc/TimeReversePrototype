using Entitas;
using Entitas.CodeGeneration.Attributes;
using TimelineData;

[Time, Unique]
public class TimelineLastPositionComponent : IComponent
{
    public PlayerTimelineData Value;
}