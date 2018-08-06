using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCtr : MonoBehaviour {
    public NImage MapImage, PlaneImage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HideAll() {
        MapImage.HideAll();
        PlaneImage.HideAll();
    }

    public void ShowAll() {

        MapImage.ShowAll();
        PlaneImage.ShowAll();

    }

    public void initialization() {

    }
}
