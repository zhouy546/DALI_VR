using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using System.IO;

using LitJson;

using UnityEngine.UI;

public class ReadJson : MonoBehaviour {
    public static ReadJson instance;

    private JsonData itemDate;

    private string jsonString;

    string ip, port, enableMouse, SplitChar,Xrot,Yrot,Zrot;





    // Use this for initialization
    void Start () {


    }

   public IEnumerator initialization() {

        if (instance == null)
        {

            instance = this;

        }

      yield return StartCoroutine(readJson());
    }


    IEnumerator readJson() {
        string spath = Application.streamingAssetsPath + "/information.json";

        Debug.Log(spath);

        WWW www = new WWW(spath);

        yield return www;

        jsonString = System.Text.Encoding.UTF8.GetString(www.bytes);

        JsonMapper.ToObject(www.text);

       itemDate = JsonMapper.ToObject(jsonString.ToString());


        ip = itemDate["config"]["IP"].ToString();//get ip;

        port =itemDate["config"]["Port"].ToString();//get port;

        enableMouse = itemDate["config"]["EnbaleMouse"].ToString();//get 是否开启鼠标控制

        SplitChar = itemDate["config"]["SplitChar"].ToString(); //UDP分隔符


       // ----------------- 获取初始摄像机角度---------------------//
        List<string> angles = new List<string>();

        for (int i = 0; i < itemDate["config"]["DefaultCameraRot"].Count; i++)
        {

            angles.Add(itemDate["config"]["DefaultCameraRot"][i].ToString());
        }

        Xrot = angles[0];

        Yrot = angles[1];

        Zrot = angles[2];

        CameraMovement_hemisphere.DefaultCameraRot = new Vector3(int.Parse(Xrot), int.Parse(Yrot), int.Parse(Zrot));
        //--------------------------------------------------------------//
        SetupData();
    }

    void SetupData() {
        GetUDPMessage.m_ReceivePort = int.Parse(port);
        CameraMovement_hemisphere.EnbaleMouseCtr= boolConvert(enableMouse);
        DealWithUDPMessage.sliceStr = charConvert(SplitChar);

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
