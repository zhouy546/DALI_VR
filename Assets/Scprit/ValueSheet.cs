﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ValueSheet   {

    public static float Cam_X_RotMaxium=30F;

    public static float Cam_Y_RotMaxium=15F;

    public static float Cam_Z_RotMaxium=10F;

    public static float Cam_X_RotMinium = -30F;

    public static float Cam_Y_RotMinium = -15F;

    public static float Cam_Z_RotMinium = -10F;

    public static bool  EnterTrigger;

    public static bool ExitTrigger;

    //public static bool EnbaleMouseCtr = false;
    public static float playBackRate = 1;

    public static float EaseingValue = 0.013f;

    public static Vector3 DefaultCameraRot;

    public static Vector3 CamRotation;

    public static char[] sliceStr;

    [Tooltip("接受端口号")] public static int m_ReceivePort = 29010;

    public static int m_TargetPort;

    public static string sentIP;

    public static string[] videoPath;

    public static int CurrentPlayID=0;

    public static Dictionary<int, List<float>> DescriptionImageTime = new Dictionary<int, List<float>>();

    public static Dictionary<int,List<Vector3>> DescriptionImageID_PosList = new Dictionary<int, List<Vector3>>();


}
