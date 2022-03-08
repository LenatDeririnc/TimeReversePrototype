using UnityEditor;

namespace InputHandler.Editor
{
    [CustomEditor(typeof(InputHandlerComponent))]
    public class InfoPanel : UnityEditor.Editor
    {
        private InputHandlerComponent _component;

        private void OnEnable()
        {
            _component = (InputHandlerComponent) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // EditorGUILayout.HelpBox( message:
            //     $"moveDirection: {_component.moveDirection.x}, {_component.moveDirection.z}",
            //     MessageType.Info);
        }
    }
}