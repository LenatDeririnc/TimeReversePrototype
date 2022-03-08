using UnityEditor;

namespace TimeSystem.Editor
{
    [CustomEditor(typeof(TimeManagerComponent))]
    public class Information : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (TimeManagerComponent.TimeManager == null)
            {
                EditorGUILayout.HelpBox("Info available only on play mode", MessageType.Info);
                return;
            }

            EditorGUILayout.HelpBox(message:
                $"Time: {TimeManagerComponent.TimeManager.time}\n" +
                $"Speed: {TimeManagerComponent.TimeManager.timeSpeed}\n" +
                $"TickRates: {TimeManagerComponent.TimeManager.tickRates}",
                MessageType.Info);
        }
    }
}