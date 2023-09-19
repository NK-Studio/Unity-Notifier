using UnityEditor;
using UnityEngine;
namespace NKStudio
{
    public class NotificationSettings : ScriptableObject
    {
        private const string FileName = "NotificationSettings";
        
        public string LogoFileName;
        public string AudioFileName;
        public string NotificationGuideName;
        public string AudioPlayGuideName;
        
        private static NotificationSettings _instance;
        public static NotificationSettings Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                
                _instance = Resources.Load<NotificationSettings>(FileName);

#if UNITY_EDITOR
                if (_instance == null)
                {
                    _instance = CreateInstance<NotificationSettings>();
                    AssetDatabase.CreateAsset(_instance, $"Assets/Plugins/BuildMessage/Resources/{FileName}.asset");
                    AssetDatabase.SaveAssets();
                }
  #endif
                return _instance;
            }
        }
    }
}
