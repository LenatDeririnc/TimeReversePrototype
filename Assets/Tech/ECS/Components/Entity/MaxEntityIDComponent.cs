using Entitas;
using Entitas.CodeGeneration.Attributes;
using Entitas.VisualDebugging.Unity;

[Unique, DontDrawComponent]
public class MaxEntityIDComponent : IComponent
{
    public int Value;
}