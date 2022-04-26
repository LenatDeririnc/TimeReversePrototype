using System;
using UnityEngine;

namespace ECS.Mono
{
    public class MonoProvider : MonoBehaviour
    {
        protected Contexts Contexts => EcsManager.Contexts;
    }
}