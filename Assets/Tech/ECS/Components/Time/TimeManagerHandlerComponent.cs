using ECS.Systems.TimeManagement;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique, Time]
public class TimeManagerHandlerComponent : IComponent
{
     public TimeManager Value;
}