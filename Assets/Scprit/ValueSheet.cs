using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ValueSheet   {

    public static float Cam_X_RotMaxium=30F;

    public static float Cam_Y_RotMaxium=15F;

    public static float Cam_Z_RotMaxium=10F;

    public static bool  EnterTrigger;

    public static bool ExitTrigger;

    //public static bool EnbaleMouseCtr = false;
    public static float playBackRate = 1;

    public static float EaseingValue = 0.013f;

    public static Vector3 DefaultCameraRot;

    public static Vector3 CamRotation;

    public static char[] sliceStr;

    [Tooltip("接受端口号")] public static int m_ReceivePort = 29010;

    public static string[] videoPath;

}
