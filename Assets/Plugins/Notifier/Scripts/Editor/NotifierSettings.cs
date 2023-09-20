using UnityEditor;
using UnityEngine;
namespace NKStudio
{
    public class NotifierSettings : ScriptableObject
    {
        private const string FileName = "NotifierSettings";
        
        public string LogoFileName;
        public string AudioFileName;
        public string NotificationGuideName;
        public string AudioPlayGuideName;
        
        private static NotifierSettings _instance;
        public static NotifierSettings Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                
                _instance = AssetDatabase.LoadAssetAtPath<NotifierSettings>($"Assets/Plugins/Notifier/Data/{FileName}.asset");
                
                return _instance;
            }
        }

        [MenuItem("Window/Notifier/Settings")]
        private static void CreateSetting()
        {
            _instance = CreateInstance<NotifierSettings>();
            AssetDatabase.CreateAsset(_instance, $"Assets/Plugins/Notifier/Data/{FileName}.asset");
            AssetDatabase.SaveAssets();
        }
    }
}
