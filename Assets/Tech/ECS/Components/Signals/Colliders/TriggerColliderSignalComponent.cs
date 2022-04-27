using Entitas;
using UnityEngine;

[Signals]
public class TriggerColliderSignalComponent : IComponent
{
    public GameEntity Sender;
    public Collider Getter;
}