using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UDPCTR : MonoBehaviour {
    public InputField InputFieldPort;
    public GetUDPMessage getUDPMessage;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnEnable()
    {
        UpdateInputText(GetPort());
    }

    public int GetPort()
    {
        int port = ValueSheet.m_ReceivePort;

        return port;
    }

    public void ChnagePort() {
        ValueSheet.m_ReceivePort = int.Parse(InputFieldPort.text);

        getUDPMessage.StopUdp();

        getUDPMessage.InitializationUdp();
    }

    public void UpdateInputText(int res)
    {
        InputFieldPort.text = res.ToString();
  
    }
}
