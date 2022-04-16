using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique, Time]
public class TickCountComponent : IComponent
{
    public int Value;
}