using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class NVideo : VideoBase{
    public VideoSlider videoSlider;

    // Use this for initialization
    new void Start () {
        base.initialization();

    }
	
	// Update is called once per frame
	void Update () {

        UpdateSlider(VideoProportion);

    }

    void UpdateSlider(float value) {
        if (!videoSlider.IsOnClicked)
        {
       videoSlider.SetValue(value);
        }
        else {
            return;
        }
    }



}
