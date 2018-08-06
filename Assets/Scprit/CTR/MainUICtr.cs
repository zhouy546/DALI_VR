using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUICtr : MonoBehaviour {
   public DescriptionImageCtr descriptionImageCtr;
    public TitleCtr titleCtr;
    public MapCtr mapCtr;

    private void OnEnable()
    {
        if (MeshVideo.instance != null)
        {
            MeshVideo.instance.VideoPlayEvent += ShowAll;
        }
    }

    public void OnDisable()
    {
        MeshVideo.instance.VideoPlayEvent -= ShowAll;
    }


    // Use this for initialization
    void Start () {
		
	}

    public void initialization() {

        titleCtr.initialization();
        mapCtr.initialization();
        descriptionImageCtr.initialization();
        MeshVideo.instance.VideoPlayEvent += ShowAll;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void HideAll() {
        titleCtr.HideAll();
        descriptionImageCtr.HideAll();
        mapCtr.HideAll();
    }

    public void ShowAll() {
        titleCtr.ShowTitle();
        descriptionImageCtr.ShowDescription();
        mapCtr.ShowAll();
    }
}
