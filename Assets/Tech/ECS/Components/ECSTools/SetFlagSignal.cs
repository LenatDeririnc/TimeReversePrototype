using System;
using Entitas;

public class SetFlagSignal : IComponent
{
    public bool Value;
    public Action<bool> Delegate;
}