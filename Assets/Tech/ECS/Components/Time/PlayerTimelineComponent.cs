using System.Collections.Generic;
using Common;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique, Time]
public class TimelineDataComponent : IComponent
{
    public Stack<TimelineData> Value;
}