using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUICtr : MonoBehaviour {
   public DescriptionImageCtr descriptionImageCtr;
    public TitleCtr titleCtr;
    // Use this for initialization
    void Start () {
		
	}

    public void initialization() {
        descriptionImageCtr.initialization();
        titleCtr.initialization();

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void HideAll() {
        titleCtr.HideAll();
        descriptionImageCtr.HideAll();
    }


}
