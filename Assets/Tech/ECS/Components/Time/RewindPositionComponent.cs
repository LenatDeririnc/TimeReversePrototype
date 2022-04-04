using Common;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Time, Unique]
public class RewindPositionComponent : IComponent
{
    public TransformInfo Value;
}