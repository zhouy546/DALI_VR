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

    string ip;

    int port;
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

             port =int.Parse(itemDate["config"]["Port"].ToString());//get port;

        SetupData();
    }

    void SetupData() {
        GetUDPMessage.m_ReceivePort = port;
    }

}
