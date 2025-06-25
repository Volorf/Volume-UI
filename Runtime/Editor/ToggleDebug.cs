using UnityEditor;
using UnityEngine;

namespace Volorf.VolumeUI
{
    [CustomEditor(typeof(Toggle))]
    public class ToggleDebug: Editor
    {
        float f = 0.1f;
        void OnSceneGUI()
        {
            Toggle t = (Toggle)target;
            Vector3 center = t.transform.position;
            Vector3 forward = t.transform.forward;
            
            // Draw Normal
            Handles.color = Color.blue;
            Handles.DrawLine(center, center + forward * f, 1f);
            
            // Projected Point
            Vector3 projectedPoint = center + forward * t.pressFactor;
            Handles.color = Color.red;
            Handles.DrawLine(center, projectedPoint, 2f);
            Handles.SphereHandleCap(0, projectedPoint, Quaternion.identity, 0.005f, EventType.Repaint);
            Handles.Label(projectedPoint + Vector3.up * 0.01f, $"Press Factor: {t.pressFactor:F2}", new GUIStyle { fontSize = 12, normal = new GUIStyleState { textColor = Color.white } });
            
            // Tap Direction
            Handles.color = Color.green;
            Handles.DrawLine(center, t.startTapPosition, 1f);
        }
    }
}