﻿using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using System.IO;

using LitJson;

using UnityEngine.UI;
using System.Linq;

public class ReadJson : MonoBehaviour {
    public static ReadJson instance;

    private JsonData itemDate;

    private string jsonString;




    // Use this for initialization
    void Start () {


    }

   public IEnumerator initialization() {

        if (instance == null)
        {

            instance = this;

        }

      yield return StartCoroutine(readJson());

        GetData();

    }


    IEnumerator readJson() {
        string spath = Application.streamingAssetsPath + "/information.json";

        Debug.Log(spath);

        WWW www = new WWW(spath);

        yield return www;

        jsonString = System.Text.Encoding.UTF8.GetString(www.bytes);

        JsonMapper.ToObject(www.text);

       itemDate = JsonMapper.ToObject(jsonString.ToString());
    }


    void GetData() {

        getPort();
        getIP();
        //getEnableMouse();

        getSplitChar();

        getCameraRot();

        getCameraEaseValue();

        getvideoPatht();

        GetCamMaxiumAngle();

        GetCamMiniumAngle();

        getDescriotionImageTime();

        getDescriotionImageStartPos();
    }

    void getvideoPatht()
    {
        List<string> paths = new List<string>();

        for (int i = 0; i < itemDate["config"]["videoName"].Count; i++)
        {

            paths.Add(itemDate["config"]["videoName"][i].ToString());
        }

        ValueSheet.videoPath = paths.ToArray();
    }
    List<float> Temptime = new List<float>();
    void getDescriotionImageTime() {
        for (int i = 0; i < itemDate["config"]["DescriotionImageTime"].Count; i++)
        {
            for (int j = 0; j < itemDate["config"]["DescriotionImageTime"][i.ToString()].Count; j++)
            {
             
               Temptime.Add(float.Parse(itemDate["config"]["DescriotionImageTime"][i.ToString()][j].ToString()));                    
            }

            float[] time = new float[Temptime.Count];

            Temptime.CopyTo(time);

            ValueSheet.DescriptionImageTime.Add(i, time.ToList());
            Temptime.Clear();
        }
    }
    List<float> tempStartPosfloat = new List<float>();
    List<Vector3> tempStartPosVector3 = new List<Vector3>();
    void getDescriotionImageStartPos() {
        for (int i = 0; i < itemDate["config"]["DescriotionImageStartPos"].Count; i++)
        {
            for (int j = 0; j < itemDate["config"]["DescriotionImageStartPos"][i.ToString()].Count; j++) {
                
                for (int k = 0; k < itemDate["config"]["DescriotionImageStartPos"][i.ToString()][j.ToString()].Count; k++)
                {
                    tempStartPosfloat.Add(float.Parse(itemDate["config"]["DescriotionImageStartPos"][i.ToString()][j.ToString()][k].ToString()));
                }

                Vector3 pos = new Vector3(tempStartPosfloat[0], tempStartPosfloat[1], 4.54f);
                tempStartPosVector3.Add(pos);
                tempStartPosfloat.Clear();
            }

            Vector3[] thepos = new Vector3[tempStartPosVector3.Count];

            tempStartPosVector3.CopyTo(thepos);

            ValueSheet.DescriptionImageID_PosList.Add(i, thepos.ToList());
            tempStartPosVector3.Clear();
        }
        //for (int i = 0; i < itemDate["config"]["DescriotionImageStartPos"].Count; i++)
        //{
        //    for (int j = 0; j < ValueSheet.DescriptionImageID_PosList[i].Count; j++)
        //    {
        //        Debug.Log("VIDEO ID : " + i.ToString() + "Image num : " + j.ToString() + "position" + ValueSheet.DescriptionImageID_PosList[i][j].ToString());
        //    }
        //}

    }

    void getCameraEaseValue() {

        string CameraEaseValue = itemDate["config"]["CamEaseingValue"].ToString();

        ValueSheet.EaseingValue = float.Parse(CameraEaseValue);
    }


    void GetCamMaxiumAngle() {
        List<string> angles = new List<string>();

        for (int i = 0; i < itemDate["config"]["CameraMaxRot"].Count; i++)
        {
            angles.Add(itemDate["config"]["CameraMaxRot"][i].ToString());
        }

        ValueSheet.Cam_X_RotMaxium =float.Parse( angles[0]);

        ValueSheet.Cam_Y_RotMaxium = float.Parse(angles[1]);

        ValueSheet.Cam_Z_RotMaxium = float.Parse(angles[2]);

    }

    void GetCamMiniumAngle()
    {
        List<string> angles = new List<string>();

        for (int i = 0; i < itemDate["config"]["CameraMinRot"].Count; i++)
        {
            angles.Add(itemDate["config"]["CameraMinRot"][i].ToString());
        }

        ValueSheet.Cam_X_RotMinium = float.Parse(angles[0]);

        ValueSheet.Cam_Y_RotMinium = float.Parse(angles[1]);

        ValueSheet.Cam_Z_RotMinium = float.Parse(angles[2]);

    }

    void getCameraRot() {
        List<string> angles = new List<string>();

        for (int i = 0; i < itemDate["config"]["DefaultCameraRot"].Count; i++)
        {

            angles.Add(itemDate["config"]["DefaultCameraRot"][i].ToString());
        }

       string Xrot = angles[0];

       string Yrot = angles[1];

       string Zrot = angles[2];

        ValueSheet.CamRotation= ValueSheet.DefaultCameraRot = new Vector3(int.Parse(Xrot), int.Parse(Yrot), int.Parse(Zrot));
    }

    void getSplitChar() {
       string SplitChar = itemDate["config"]["SplitChar"].ToString(); //UDP分隔符
        ValueSheet.sliceStr = charConvert(SplitChar);
    }

    //void getEnableMouse() {
    // string   enableMouse = itemDate["config"]["EnbaleMouse"].ToString();//get 是否开启鼠标控制
    //    ValueSheet.EnbaleMouseCtr = boolConvert(enableMouse);

    //}

    void getPort() {
     string   port = itemDate["config"]["Port"].ToString();//get port;
        ValueSheet.m_ReceivePort = int.Parse(port);

        string TargetPort = itemDate["config"]["TargetPort"].ToString();
        ValueSheet.m_TargetPort = int.Parse(TargetPort);
    }

    void getIP() {
      string  ip = itemDate["config"]["IP"].ToString();

        //get ip;
        ValueSheet.sentIP = ip;
    }

    bool boolConvert(string s) {
        if (s == "0")
        {
            return false;
        }
        else if (s == "1") {
            return true;
        }

        return false;
    }

    char[] charConvert(string s) {
        char[] mchar = s.ToCharArray();

        return mchar;
    }
}
