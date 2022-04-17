using System.Collections.Generic;
using Common;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using TimelineData;
using Tools.TimeLineStackTool;

[Unique, Time]
public class PlayerTimelineDataComponent : IComponent
{
    public TimeLineStack Value;
}