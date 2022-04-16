using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique, Time]
public class PreviousTickCountComponent : IComponent
{
    public int Value;
}