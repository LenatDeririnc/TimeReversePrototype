using UnityEditor;

namespace TimeSystem.Editor
{
    [CustomEditor(typeof(TimeManager))]
    public class Information : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (TimeManager.TimeHandler == null)
            {
                EditorGUILayout.HelpBox("Info available only on play mode", MessageType.Info);
                return;
            }

            EditorGUILayout.HelpBox(message:
                $"Time: {TimeManager.TimeHandler.time}\n" +
                $"Speed: {TimeManager.TimeHandler.timeSpeed}\n" +
                $"TickRates: {TimeManager.TimeHandler.tickRates}",
                MessageType.Info);
        }
    }
}