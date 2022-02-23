using UnityEngine;

namespace BootstrapperSystem
{
    [ExecuteInEditMode]
    public class BootstrapperComponent : MonoBehaviour
    {
        private Bootstrapper _bootstrapper;

        private void Awake()
        {
            _bootstrapper = new Bootstrapper(this);
        }
    }
}