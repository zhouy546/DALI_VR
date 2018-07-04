//*********************❤*********************
// 
// 文件名（File Name）：	DealWithUDPMessage.cs
// 
// 作者（Author）：			LoveNeon
// 
// 创建时间（CreateTime）：	Don't Care
// 
// 说明（Description）：	接受到消息之后会传给我，然后我进行处理
// 
//*********************❤*********************

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class DealWithUDPMessage : MonoBehaviour {
  
    private string dataTest;
    public static char[] sliceStr;
    private Vector3 CamRotation;
    /// <summary>
    /// 消息处理
    /// </summary>
    /// <param name="_data"></param>
    public void MessageManage(string _data)
    {
        if (_data != "") {
            dataTest = _data;

            string[] strs = dataTest.Split(sliceStr);
            for (int i = 0; i < strs.Length; i++)
            {
                if (i == 0)
                    CamRotation.x =int.Parse( strs[i]);

                if (i == 1)
                    CamRotation.y = int.Parse(strs[i]);

                if (i == 2)
                    CamRotation.z = int.Parse(strs[i]);
            }
            CameraMovement_hemisphere.CamRotation = CamRotation;
            Debug.Log(CamRotation);
        }

    }
    private void Update()
    {
       // Debug.Log("数据：" + dataTest);  
    }

}
