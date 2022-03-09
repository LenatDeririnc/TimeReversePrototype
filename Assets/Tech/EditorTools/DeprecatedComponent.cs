using System;
using UnityEngine;

namespace EditorTools
{
    [ExecuteInEditMode]
    public abstract class DeprecatedComponent : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(this);
        }
    }
}