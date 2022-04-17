using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique, Time]
public class SmoothRewindSpeedComponent : IComponent
{
    public float value;
}