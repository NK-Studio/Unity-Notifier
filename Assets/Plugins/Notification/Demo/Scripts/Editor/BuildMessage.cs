using UnityEditor;
#if UNITY_EDITOR_WIN && BAKERY_INCLUDED
using UnityEditor.SceneManagement;
#endif

namespace NKStudio
{
    public class BuildMessage : Editor
    {
        [InitializeOnLoadMethod]
        public static void LightBakeMessageOn()
        {
            Lightmapping.bakeCompleted += LightBakeComplete;
            Lightmapping.bakeStarted += LightBakeStart;
#if UNITY_EDITOR_WIN && BAKERY_INCLUDED
            ftRenderLightmap.OnPreFullRender += (sender, args) =>
            {
                EditorSceneManager.SaveOpenScenes();
                LightBakeStart();
            };
            ftRenderLightmap.OnFinishedFullRender += (sender, args) => LightBakeComplete();
#endif
        }

        [MenuItem("Tools/Light Bake Start")]
        private static void LightBakeStart()
        {
            NotificationUtility.ShowNotificationWithAudio("Unity","알림","라이트맵 베이크를 시작합니다.");
        }

        [MenuItem("Tools/Light Bake Complete")]
        private static void LightBakeComplete()
        {
            NotificationUtility.ShowNotificationWithAudio("Unity","알림","라이트맵 베이크가 완료되었습니다.");
        }
    }
}
