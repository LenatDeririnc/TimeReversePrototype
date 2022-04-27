using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Unique]
public class ColliderDataComponent : IComponent
{
    public Dictionary<Collider, GameEntity> Values;
}