using System.Diagnostics;
using System.Text;
using UnityEngine;

#if UNITY_STANDALONE_WIN
using System;
using System.IO;
#endif

namespace NKStudio
{
    public static class NotificationUtility
    {
        public static void ShowNotification(string title, string subTitle, string message)
        {
#if UNITY_STANDALONE_WIN
            var notificationGuidPath =
                $"{Application.streamingAssetsPath}/Notification/Template/Windows/{NotificationSettings.Instance.NotificationGuideName}";

            var logoImagePath =
                $"{Application.streamingAssetsPath}/Notification/{NotificationSettings.Instance.ImageName}";

            var commendGuide = File.ReadAllText(notificationGuidPath);
            var commend = commendGuide
                .Replace("$Your Project$", subTitle)
                .Replace("$Inner Message$", message)
                .Replace("$Icon Path$", logoImagePath)
                .Replace("$Unity Version$", Application.unityVersion);

            ExecuteCommendScript(commend);

#elif UNITY_STANDALONE_OSX
            ExecuteCommendScript(title, subTitle, message);
#endif
        }

        public static void ShowNotification(string title, string message)
        {
#if UNITY_STANDALONE_WIN
            var notificationGuidPath =
                $"{Application.streamingAssetsPath}/Notification/Template/Windows/{NotificationSettings.Instance.NotificationGuideName}";

            var logoImagePath =
                $"{Application.streamingAssetsPath}/Notification/{NotificationSettings.Instance.ImageName}";

            var commendGuide = File.ReadAllText(notificationGuidPath);
            var commend = commendGuide
                .Replace("$Your Project$", title)
                .Replace("$Inner Message$", message)
                .Replace("$Icon Path$", logoImagePath)
                .Replace("$Unity Version$", Application.unityVersion);

            ExecuteCommendScript(commend);

#elif UNITY_STANDALONE_OSX
            ExecuteCommendScript(title, message);
#endif
        }

        public static void PlayAudio()
        {
#if UNITY_STANDALONE_WIN
            var audioPlayGuidPath =
                $"{Application.streamingAssetsPath}/Notification/Template/Windows/{NotificationSettings.Instance.AudioPlayGuideName}";
            var audioPath = $"{Application.streamingAssetsPath}/Notification/{NotificationSettings.Instance.AudioName}";

            string commendGuide = File.ReadAllText(audioPlayGuidPath);
            var commend = commendGuide
                .Replace("$AudioSound$", audioPath);
            ExecuteCommendScript(commend);

#elif UNITY_STANDALONE_OSX
            Process.Start("afplay", $"{Application.streamingAssetsPath}/Notification/{NotificationSettings.Instance.AudioName}");
#endif
        }

#if UNITY_STANDALONE_WIN
        private static void ExecuteCommendScript(string script)
        {
            byte[] byteArray = Encoding.Unicode.GetBytes(script);

            string scriptBase64Encoded = Convert.ToBase64String(byteArray);

            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy unrestricted -EncodedCommand {scriptBase64Encoded}",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process.Start(processStartInfo);
        }
#elif UNITY_STANDALONE_OSX
        /// <summary>
        /// textKey : 실행할 명령어 
        /// </summary>
        private static void ExecuteCommendScript(string title, string subTitle, string message)
        {
            var appPath = Application.streamingAssetsPath + "/Notification/.Plugins/unity-notifier.app";
            string arguments = $"-title '{title}' -subtitle '{subTitle}' -message '{message}' -ignoreDnD";

            Process process = new Process {
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
        
        /// <summary>
        /// textKey : 실행할 명령어 
        /// </summary>
        private static void ExecuteCommendScript(string title, string message)
        {
            var appPath = Application.streamingAssetsPath + "/Notification/.Plugins/unity-notifier.app";
            string arguments = $"-title '{title}' -message '{message}' -ignoreDnD";

            Process process = new Process {
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
}