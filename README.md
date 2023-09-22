# Unity Notifier
유니티에서 노티 알림을 띄웁니다.

### UnityPackage -> [UnityPackage](https://github.com/NK-Studio/Unity-Notifier/releases) 다운로드

Platform Support
- Windows 10, 11
- MacOS Ventura

# 주의사항
## Window
1. 타이틀 이름을 쉽게 변경이 어렵습니다.
![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/0f514483-63aa-474d-8d2a-71c86153c88e/c144a65d-9d46-4b1e-a79e-459cad47d69a/Untitled.png)
Notification Guide.txt에 보면 위와 같은 내용들이 있습니다.
`6D809377-6AF0-444B-8957-A3773F02200E` 는 **Program Files를 의미**합니다.
**APP_ID는 알림을 받고 싶어하는 프로그램의  exe 경로를 지정하면 됩니다.**
   
## MacOS
1. **.gitIgnore에서 *.app 제거**하기
MacOS에서 알림을 전달하는 방식은 실제 유니티에서 알림을 전달하는 것이 아니라,
unity-notifier.app을 통해 알림을 전달합니다. (이유는, AppDelegate접근이 파악이 안되서
포그라운드에서 알림을 전달할 방법이 마땅치 않음.)

그래서 `Assets/Plugins/Notifier/Plugins/unity-notifier.app` 파일에 의존합니다.
그런데 Unity템플릿용 .gitIgnore를 보면 대부분 *.app이 포함되어 있습니다.
2. 화면을 미러링 하거나 녹화 중일 때 알림이 안 나올 수 있습니다.
시스템 설정-알림에서 `디스플레이를 미러링하거나 공유할 때 알림 허용` 을 **활성화** 해야합니다.
