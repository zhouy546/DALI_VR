using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasCtr : MonoBehaviour {
    public GameObject console;
    bool isConsoleOn;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            isConsoleOn = !isConsoleOn;
            console.SetActive(isConsoleOn);
        }


	}
}
