using Entitas;
using Entitas.CodeGeneration.Attributes;
using Tools.TimeLineStackTool;

[Unique, Time]
public class TimeLineStackComponent : IComponent
{
    public TimeLineStack Value;
}