using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace NKStudio
{
    public class NotifierAbout : EditorWindow
    {
        private VisualTreeAsset _notifierAboutUXML;
        private StyleSheet _notifierAboutUSS;
        private TextAsset _packageJson;

        private void Awake()
        {
            _notifierAboutUXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Plugins/Notifier/Scripts/Editor/About/NotifierAbout.uxml");
            _notifierAboutUSS = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Plugins/Notifier/Scripts/Editor/About/NotifierAbout.uss");
            _packageJson = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Plugins/Notifier/package.json");
        }

        [MenuItem("Window/Notifier/About")]
        public static void Init()
        {
            NotifierAbout wnd = GetWindow<NotifierAbout>();
            wnd.titleContent = new GUIContent("About");
            wnd.minSize = new Vector2(350, 120);
            wnd.maxSize = new Vector2(350, 120);
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            VisualTreeAsset visualTree = _notifierAboutUXML;
            root.styleSheets.Add(_notifierAboutUSS);
            VisualElement container = visualTree.Instantiate();
            root.Add(container);

            string version = GetVersion();
            root.Q<Label>("version-label").text = $"Version : {version}";
        }

        private string GetVersion()
        {
            PackageInfo packageInfo = JsonUtility.FromJson<PackageInfo>(_packageJson.text);
            return packageInfo.version;
        }
    }

    [System.Serializable]
    public class PackageInfo
    {
        public string name;
        public string displayName;
        public string version;
        public string unity;
        public string description;
        public List<string> keywords;
        public string type;
    }
}
