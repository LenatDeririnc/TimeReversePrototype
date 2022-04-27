using Entitas;

[Signals]
public class TriggerEntitySignalComponent : IComponent
{
    public GameEntity Sender;
    public GameEntity Getter;
}