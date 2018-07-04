using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCtr : MonoBehaviour {
    GetUDPMessage getUDPMessage;

    Ini ini;
    // Use this for initialization
    void Start () {
        getUDPMessage = FindObjectOfType<GetUDPMessage>();

        ini = FindObjectOfType<Ini>();

    }

    public void ResetAll()
    {
        getUDPMessage.StopUdp();

        StartCoroutine(ini.initialization());
    }

    // Update is called once per frame
    void Update () {
		
	}
}
