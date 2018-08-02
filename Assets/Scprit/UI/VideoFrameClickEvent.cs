using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Canvas))]
public class VideoFrameClickEvent : MonoBehaviour {
    public int id;

   public string path;

    MeshVideo meshVideo;

    Canvas canvas;
	// Use this for initialization
	void Start () {
        meshVideo = FindObjectOfType<MeshVideo>();
        canvas = this.GetComponent<Canvas>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetLayer(int value)
    {
        canvas.overrideSorting = true;

        canvas.sortingOrder = value;
    }

    public void PlayVideo() {
        meshVideo.SetVideoPath(path);
        meshVideo.LoadVideo(meshVideo.path, true);
    }
}
