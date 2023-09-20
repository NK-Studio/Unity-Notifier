using System.Linq;
using UnityEditor;
using UnityEngine;
namespace NKStudio
{
    public class NotifierSettings : ScriptableObject
    {
        private const string FileName = "NotifierSettings";
        private static readonly string FullPath = $"Assets/Plugins/Notifier/Data/{FileName}.asset";

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

                _instance = AssetDatabase.LoadAssetAtPath<NotifierSettings>(FullPath);

                return _instance;
            }
        }

        [MenuItem("Window/Notifier/Settings")]
        private static void CreateSetting()
        {
            _instance = AssetDatabase.LoadAssetAtPath<NotifierSettings>(FullPath);
            if (_instance != null)
            {
                Selection.activeObject = _instance;
                Debug.Log("이미 존재합니다.");
                return;
            }

            // 폴더 생성
            EnsureNestedDirectoriesExist("Assets/Plugins/Notifier/Data");

            _instance = CreateInstance<NotifierSettings>();
            AssetDatabase.CreateAsset(_instance, FullPath);
            AssetDatabase.SaveAssets();
            Debug.Log($"{FullPath}에 생성되었습니다.");
            Selection.activeObject = _instance;
        }

        private static void EnsureNestedDirectoriesExist(string path)
        {
            string[] directories = path.Split(new char[] { '/' }, System.StringSplitOptions.RemoveEmptyEntries);
            string currentPath = "";
            
            foreach (var directory in directories)
            {
                currentPath = string.IsNullOrEmpty(currentPath) ? directory : $"{currentPath}/{directory}";
                if (!AssetDatabase.IsValidFolder(currentPath))
                {
                    var parentDirectories = currentPath.Split('/').Take(currentPath.Split('/').Length - 1);
                    var parentDirectory = string.Join("/", parentDirectories);
                    var newDirectory = currentPath.Split('/').Last();
                    AssetDatabase.CreateFolder(parentDirectory, newDirectory);
                }
            }
        }
    }
}
