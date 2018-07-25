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
   // public static char[] sliceStr;
    private Vector3 CamRotation;
    //private bool enterTrigger, exitTrigger;
    /// <summary>
    /// 消息处理
    /// </summary>
    /// <param name="_data"></param>
    public void MessageManage(string _data)
    {
        if (_data != "") {
            dataTest = _data;

            string[] strs = dataTest.Split(ValueSheet.sliceStr);
            for (int i = 0; i < strs.Length; i++)
            {
                try
                {
                    if (i == 0)
                        CamRotation.x =UtilityFunction.Mapping( float.Parse(strs[i]),-1,1,-30,30);

                    if (i == 1)
                        CamRotation.y = UtilityFunction.Mapping(float.Parse(strs[i]),-1,1,-30,30);

                    if (i == 2)
                        CamRotation.z = UtilityFunction.Mapping(float.Parse(strs[i]), -1, 1, -5, 5);

                    if (i == 3) {
                        ValueSheet.EnterTrigger =Convert.ToBoolean( int.Parse(strs[i]));
                    }
                    if (i == 4) {
                        ValueSheet.ExitTrigger = Convert.ToBoolean(int.Parse(strs[i]));
                    }

                    Debug.Log("GET DATA");
                }
                catch (Exception)
                {
                    dataTest = "data format wrong: x y z 1/0 1/0" +"\n"
                        +"current udpData: "+_data.ToString();
                }

            }
            

            ValueSheet.CamRotation = CamRotation;
            Debug.Log(CamRotation);
        }

    }
    private void Update()
    {


       // Debug.Log("数据：" + dataTest);  
    }
    private void OnGUI()
    {
        GUIStyle bb = new GUIStyle();
        bb.normal.background = null;    //这是设置背景填充的
        bb.normal.textColor = new Color(1.0f, 0.5f, 0.0f);   //设置字体颜色的
        bb.fontSize = 40;       //当然，这是字体大小

        //居中显示FPS
        GUI.Label(new Rect((Screen.width / 2) - 40, 0, 200, 200), "udp message: " + dataTest, bb);

    }
}
