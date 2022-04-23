using CharacterSystem.Player;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class PlayerModelComponent : IComponent
{
    public PlayerModel Value;
}