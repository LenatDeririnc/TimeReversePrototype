﻿using UnityEngine;

namespace Tools.SingletonTool
{
    public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null) Destroy(this);
            base.Awake();
        }
    }
}