using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique, Input]
public class InputSettingsComponent : IComponent
{
    public InputSettings Value;
}