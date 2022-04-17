using CharacterSystem.Player;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Unique, Input]
public class InputControllingComponent : IComponent
{
    public IInputControlling Value;
}