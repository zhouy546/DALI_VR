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

        getEnableMouse();

        getSplitChar();

        getCameraRot();

        getCameraEaseValue();

        getvideoPatht();
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

        void getCameraEaseValue() {

        string CameraEaseValue = itemDate["config"]["CamEaseingValue"].ToString();

        ValueSheet.EaseingValue = float.Parse(CameraEaseValue);
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

        ValueSheet.DefaultCameraRot = new Vector3(int.Parse(Xrot), int.Parse(Yrot), int.Parse(Zrot));
    }

    void getSplitChar() {
       string SplitChar = itemDate["config"]["SplitChar"].ToString(); //UDP分隔符
        ValueSheet.sliceStr = charConvert(SplitChar);
    }

    void getEnableMouse() {
     string   enableMouse = itemDate["config"]["EnbaleMouse"].ToString();//get 是否开启鼠标控制
        ValueSheet.EnbaleMouseCtr = boolConvert(enableMouse);

    }

    void getPort() {
     string   port = itemDate["config"]["Port"].ToString();//get port;
        ValueSheet.m_ReceivePort = int.Parse(port);

    }

    void getIP() {
      string  ip = itemDate["config"]["IP"].ToString();//get ip;
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
