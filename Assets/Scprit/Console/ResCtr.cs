using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResCtr : MonoBehaviour {
    public InputField WidthInput, HeightInput;

	// Use this for initialization
	void Start () {
		
	}
	
    public int[] GetRes() { 
    int[] res = new int[2];
        res[0] = Screen.width;
        res[1] = Screen.height;
        return res;
    }

    public void OnEnable()
    {
        UpDateInputText(GetRes());
    }

    public void UpDateInputText(int[] res) {
        WidthInput.text = res[0].ToString();
        HeightInput.text = res[1].ToString();
    }

    public void SetRes() {
        try
        {
            Screen.SetResolution(int.Parse(WidthInput.text), int.Parse(HeightInput.text), false);
        }
        catch (System.Exception e)
        {

            throw e;
        }
    }
}
