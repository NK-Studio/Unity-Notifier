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
            
            //베이커리 에셋 사용시 활성화 됨
#if UNITY_EDITOR_WIN && BAKERY_INCLUDED
            ftRenderLightmap.OnPreFullRender += (sender, args) =>
            {
                EditorSceneManager.SaveOpenScenes();
                LightBakeStart();
            };
            ftRenderLightmap.OnFinishedFullRender += (sender, args) => LightBakeComplete();
#endif
        }
        
        [MenuItem("Tools/Notifier/Show01")]
        private static void LightBakeStart()
        {
            // Notifier.Show("Unity","알림","라이트맵 베이크를 시작합니다."); // MacOS Only
            Notifier.Show("알림","라이트맵 베이크를 시작합니다.");
        }
        
        [MenuItem("Tools/Notifier/Show02")]
        private static void LightBakeComplete()
        {
            NotifierUtility.UniversalShow("Unity","알림","라이트맵 베이크가 완료되었습니다.");
        }
    }
}
