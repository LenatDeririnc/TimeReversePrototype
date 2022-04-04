using Entitas;
using Entitas.CodeGeneration.Attributes;
using TimeSystem;

[Unique, Time]
public class TimeManagerHandlerComponent : IComponent
{
     public TimeManager Value;
}