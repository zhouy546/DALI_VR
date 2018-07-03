using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VideoSlider : SliderBase {

	// Use this for initialization
	new void Start () {
        base.initialization();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnPointerDown(PointerEventData eventData) {
        IsOnClicked = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        IsOnClicked = false;
    }

    public  void OnValueChange(NVideo nVideo) {

        if (IsOnClicked) {

            nVideo.SetMovieTime(slider.value);

        }
    }
}
