using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine.InputSystem;

[Unique, Input]
public class InputActionLookComponent : IComponent
{
    public InputAction Value;
}