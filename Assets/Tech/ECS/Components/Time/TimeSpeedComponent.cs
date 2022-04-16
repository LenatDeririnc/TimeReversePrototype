using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique, Time]
public class TimeSpeedComponent : IComponent
{
    public float Value;
}