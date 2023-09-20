using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

#if UNITY_EDITOR_WIN
using System.Text;
using UnityEditor;
using System;
#endif

namespace NKStudio
{
    public static class Notifier
    {
#if UNITY_EDITOR_OSX
        private static readonly string AppPath = $"{Application.dataPath}/Plugins/Notifier/Plugins/unity-notifier.app";
        private static readonly string AudioPath = $"{Application.dataPath}/Plugins/Notifier/Audio/SFX/{NotifierSettings.Instance.AudioFileName}";
#endif

#if UNITY_EDITOR_OSX
        /// <summary>
        /// 알림 메세지를 띄웁니다. (MacOS Only)
        /// </summary>
        /// <param name="title">메세지 타이틀</param>
        /// <param name="subTitle">부제목</param>
        /// <param name="message">내용</param>
        /// <param name="audioPlay">오디오를 재생합니다.</param>
        public static void Show(string title, string subTitle, string message, bool audioPlay = true)
#elif UNITY_EDITOR_WIN
        /// <summary>
        /// 알림 메세지를 띄웁니다.
        /// </summary>
        /// <param name="subTitle">부제목</param>
        /// <param name="message">내용</param>
        public static void Show(string subTitle, string message, bool audioPlay = true)
#endif
        {
#if UNITY_EDITOR_WIN
            string notificationGuidPath =
                $"Assets/Plugins/Notifier/Scripts/Editor/Template/{NotifierSettings.Instance.NotificationGuideName}";

            string logoImagePath =
                $"{Application.dataPath}/Plugins/Notifier/Art/Textures/{NotifierSettings.Instance.LogoFileName}";

            var commendGuide = AssetDatabase.LoadAssetAtPath<TextAsset>(notificationGuidPath);
            if (commendGuide)
            {
                if (!File.Exists(logoImagePath))
                {
                    Debug.LogError("로고 이미지가 존재하지 않습니다.");
                    return;
                }

                string commend = commendGuide.text
                    .Replace("$Your Project$", subTitle)
                    .Replace("$Inner Message$", message)
                    .Replace("$Icon Path$", logoImagePath)
                    .Replace("$Unity Version$", Application.unityVersion);

                ExecuteCommendScript(commend);
            }

#elif UNITY_EDITOR_OSX
            ExecuteCommendScript(title, subTitle, message);
#endif

            if (audioPlay)
                PlayAudio();
        }

#if UNITY_EDITOR_OSX
        /// <summary>
        /// 알림 메세지를 띄웁니다.
        /// </summary>
        /// <param name="title">메세지 타이틀</param>
        /// <param name="message">내용</param>
        /// <param name="audioPlay">오디오를 재생합니다.</param>
        public static void Show(string title, string message, bool audioPlay = true)
        {
            ExecuteCommendScript(title, message);

            if (audioPlay)
                PlayAudio();
        }
#endif

        /// <summary>
        /// 오디오를 재생합니다.
        /// </summary>
        private static void PlayAudio()
        {
#if UNITY_EDITOR_WIN
            string audioPlayGuidPath =
                $"Assets/Plugins/Notifier/Scripts/Editor/Template/{NotifierSettings.Instance.AudioPlayGuideName}";
            string audioPath =
                $"{Application.dataPath}/Plugins/Notifier/Audio/SFX/{NotifierSettings.Instance.AudioFileName}";

            TextAsset commendGuide = AssetDatabase.LoadAssetAtPath<TextAsset>(audioPlayGuidPath);
            if (commendGuide)
            {
                if (!File.Exists(audioPath))
                {
                    Debug.LogError("오디오 파일이 존재하지 않습니다.");
                    return;
                }

                string commend = commendGuide.text
                    .Replace("$AudioSound$", audioPath);
                ExecuteCommendScript(commend);
            }
#elif UNITY_EDITOR_OSX
            if (File.Exists(AudioPath))
                Process.Start("afplay", AudioPath);
            else
                Debug.LogError("오디오 파일이 존재하지 않습니다.");
#endif
        }

#if UNITY_EDITOR_WIN
        private static void ExecuteCommendScript(string script)
        {
            byte[] byteArray = Encoding.Unicode.GetBytes(script);

            string scriptBase64Encoded = Convert.ToBase64String(byteArray);

            Process process = new()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy unrestricted -EncodedCommand {scriptBase64Encoded}",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();
            process.Close();
        }
#endif

#if UNITY_EDITOR_OSX
        /// <summary>
        /// 타이틀과 메세지만 띄웁니다.
        /// </summary>
        /// <param name="title">제목</param>
        /// <param name="message">내용</param>
        private static void ExecuteCommendScript(string title, string message)
        {
            string arguments = $"-title '{title}' -message '{message}' -ignoreDnD";
            TriggerNotifier(AppPath, arguments);
        }

        /// <summary>
        /// textKey : 실행할 명령어 
        /// </summary>
        private static void ExecuteCommendScript(string title, string subTitle, string message)
        {
            string arguments = $"-title '{title}' -subtitle '{subTitle}' -message '{message}' -ignoreDnD";
            TriggerNotifier(AppPath, arguments);
        }

        /// <summary>
        /// 알림을 트리거 합니다.
        /// </summary>
        /// <param name="appPath">앱 경로</param>
        /// <param name="arguments">인자 값</param>
        private static void TriggerNotifier(string appPath, string arguments)
        {
            Process process = new() {
                StartInfo = new ProcessStartInfo {
                    FileName = $"{appPath}/Contents/MacOS/unity-notifier", // 실행 파일의 경로를 지정합니다.
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();
            process.Close();
        }
#endif
    }

    public static class NotifierUtility
    {
        /// <summary>
        /// 알림 메세지를 띄웁니다.
        /// 윈도우는 subTitle, message만 사용합니다.
        /// 맥은 title, subTitle, message를 사용합니다.
        /// </summary>
        /// <param name="title">제목</param>
        /// <param name="subTitle">부제목</param>
        /// <param name="message">내용</param>
        /// <param name="audioPlay">오디오를 재생합니다.</param>
        public static void UniversalShow(string title, string subTitle, string message, bool audioPlay = true)
        {
#if UNITY_EDITOR_WIN
            Notifier.Show(subTitle, message, audioPlay);
#elif UNITY_EDITOR_OSX
            Notifier.Show(title, subTitle, message, audioPlay);
#endif
        }
    }
}
