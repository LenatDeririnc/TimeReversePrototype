using UnityEngine;

namespace Common
{
    public static class InputContainer
    {
        public static float MouseX => Input.GetAxis("Mouse X");
        public static float MouseY => Input.GetAxis("Mouse Y");
        public static float HorizontalMove => Input.GetAxisRaw("Horizontal");
        public static float HorizontalLook => Input.GetAxis("HorizontalLook");
        public static float VerticalMove => Input.GetAxisRaw("Vertical");
        public static float VerticalLook => Input.GetAxis("VerticalLook");
        public static bool Jump => Input.GetButtonDown("Jump");
    }
}