using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoFrameClickEvent : MonoBehaviour {
   public string path;

    MeshVideo meshVideo;
	// Use this for initialization
	void Start () {
        meshVideo = FindObjectOfType<MeshVideo>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   public void PlayVideo() {
        meshVideo.SetVideoPath(path);
        
        meshVideo.LoadVideo(meshVideo.path, true);
    }
}
