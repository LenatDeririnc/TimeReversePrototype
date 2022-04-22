using UnityEngine;

namespace Common
{
    public abstract class LookStrategy
    {
        public abstract float HorizontalLook { get; }
        public abstract float VerticalLook { get; }
    }

    public class MouseLook : LookStrategy
    {
        public override float HorizontalLook => Input.GetAxis("Mouse X");
        public override float VerticalLook => Input.GetAxis("Mouse Y");
    }

    public class GamepadLook : LookStrategy
    {
        public override float HorizontalLook => Input.GetAxis("HorizontalLook");
        public override float VerticalLook => Input.GetAxis("VerticalLook");
    }
}