using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace NKStudio
{
    [CustomEditor(typeof(NotifierSettings))]
    public class NotifierSettingsEditor : Editor
    {
        private VisualElement _root;

        private SerializedProperty _logoFileNameProperty;
        private SerializedProperty _audioFileNameProperty;
        private SerializedProperty _notificationGuideNameProperty;
        private SerializedProperty _audioPlayGuideNameProperty;

        private void InitIcon()
        {
            GUIContent icon = EditorGUIUtility.IconContent("GameManager Icon");
            EditorGUIUtility.SetIconForObject(target, icon.image as Texture2D);
        }

        private void FindProperty()
        {
            _logoFileNameProperty = serializedObject.FindProperty("LogoFileName");
            _audioFileNameProperty = serializedObject.FindProperty("AudioFileName");
            _notificationGuideNameProperty = serializedObject.FindProperty("NotificationGuideName");
            _audioPlayGuideNameProperty = serializedObject.FindProperty("AudioPlayGuideName");
        }

        private void InitialRoot()
        {
            _root = new VisualElement();

            Label macAndWindows = new();
            macAndWindows.text = "Mac & Windows";
            macAndWindows.style.fontSize = 20;

            VisualElement space = new();
            space.style.height = 10;

            Label windowsOnly = new();
            windowsOnly.text = "Windows";
            windowsOnly.style.fontSize = 20;

            PropertyField logoFileNameField = new();
            logoFileNameField.BindProperty(_logoFileNameProperty);
            logoFileNameField.tooltip = "윈도우는 메세지 아이콘을 변경할 수 있습니다.";

            PropertyField audioFileNameField = new();
            audioFileNameField.BindProperty(_audioFileNameProperty);
            audioFileNameField.tooltip = "알림이 나타날 때 재생할 소리 파일 이름";

            PropertyField notificationGuideNameField = new();
            notificationGuideNameField.BindProperty(_notificationGuideNameProperty);

            PropertyField audioPlayGuideNameField = new();
            audioPlayGuideNameField.BindProperty(_audioPlayGuideNameProperty);

            HelpBox helpBox = new();
            helpBox.messageType = HelpBoxMessageType.Info;
#if UNITY_EDITOR_WIN
            helpBox.text = "윈도우는 타이틀 제목 변경이 번거롭습니다.";
#elif UNITY_EDITOR_OSX
            helpBox.text = "이 에셋은 내부에 app프로그램이 존재하며\nIgnore에서 <b><i>*.app</i></b> 내용이 제거되어야합니다.";
#endif
            _root.Add(macAndWindows);
            _root.Add(audioFileNameField);
            _root.Add(space);
            _root.Add(windowsOnly);
            _root.Add(logoFileNameField);
            _root.Add(notificationGuideNameField);
            _root.Add(audioPlayGuideNameField);

            _root.Add(space);
            _root.Add(helpBox);
        }
        
        public override VisualElement CreateInspectorGUI()
        {
            InitIcon();
            FindProperty();
            InitialRoot();

            return _root;
        }
        
        private void ClipboardCopy(string text)
        {
            TextEditor te = new();
            te.text = text;
            te.SelectAll();
            te.Copy();
        }
    }
}
