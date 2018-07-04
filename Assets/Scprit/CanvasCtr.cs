using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasCtr : MonoBehaviour {
    public GameObject console;
    public GameObject Menu;
    bool isConsoleOn;
    bool isMenuOn;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isConsoleOn = !isConsoleOn;
            console.SetActive(isConsoleOn);
        }
        else if(Input.GetKeyDown(KeyCode.Space)) {
            isMenuOn = !isMenuOn;
            Menu.SetActive(isMenuOn);

            if (isMenuOn)
            {
                PostProcessingCtr.instance.Blur();
            }
            else {
                PostProcessingCtr.instance.Focus();
            }
        }


	}
}
