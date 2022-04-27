using System;
using Entitas;

[Signals]
public class SetFlagSignal : IComponent
{
    public bool Value;
    public Action<bool> Delegate;
}