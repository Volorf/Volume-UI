using UnityEditor;
using UnityEngine;

namespace Volorf.VolumeUI
{
    [CustomEditor(typeof(ToggleGroup))]
    public class ToggleGroupInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            ToggleGroup toggleGroup = (ToggleGroup)target;
            
            DrawDefaultInspector();
            EditorGUILayout.Space();
            
            if (GUILayout.Button("Get Toggles In Children"))
            {
                toggleGroup.CollectAllTogglesInChildren();
            }
        }
    }
}

