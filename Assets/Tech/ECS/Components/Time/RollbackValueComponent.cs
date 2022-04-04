using Entitas;
using Entitas.CodeGeneration.Attributes;

[Time, Unique]
public class RollbackValueComponent : IComponent
{
    public float Value;
}