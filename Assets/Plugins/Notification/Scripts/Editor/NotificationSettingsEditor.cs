using UnityEditor;
using UnityEngine;

namespace NKStudio
{
    [CustomEditor(typeof(NotificationSettings))]
    public class NotificationSettingsEditor : Editor
    {
        private void OnEnable()
        {
            GUIContent icon = EditorGUIUtility.IconContent("GameManager Icon");
            EditorGUIUtility.SetIconForObject(target, icon.image as Texture2D);
        }
    }
}
