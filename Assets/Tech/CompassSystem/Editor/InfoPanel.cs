using CompassSystem;
using UnityEditor;
using UnityEngine;

namespace MovementCompassSystem.Editor
{
    [CustomEditor(typeof(CompassComponent))]
    public class Information : UnityEditor.Editor
    {
        private CompassComponent _component;

        private void OnEnable()
        {
            _component = (CompassComponent) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_component.Velocity == null)
                return;

            if (_component.RollbackSector.transform == null)
                return;

            EditorGUILayout.HelpBox(message: $"_velocity.Velocity(): {_component.Velocity.Velocity()}\n" +
                                             $"_velocity.Velocity().magnitude: {_component.Velocity.Velocity().magnitude}\n" +
                                             $"RollbackSector.Edges: {_component.RollbackSector.Edges}\n" +
                                             $"RollbackSector.GetAngle(_velocity.Velocity()): {_component.RollbackSector.GetAngle(_component.Velocity.Velocity())}"
                ,
                MessageType.Info);
        }
    }
}