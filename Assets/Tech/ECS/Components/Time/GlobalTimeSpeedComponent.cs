using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique, Time]
public class GlobalTimeSpeedComponent : IComponent
{
    public float Value;
}