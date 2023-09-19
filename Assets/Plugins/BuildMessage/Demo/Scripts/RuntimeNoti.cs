using System.Collections;
using System.Collections.Generic;
using NKStudio;
using UnityEngine;

public class RuntimeNoti : MonoBehaviour
{
    public void ShowNotification()
    {
        NotificationUtility.ShowNotification("Unity","테스트입니다.");
        NotificationUtility.PlayAudio();
    }
}

