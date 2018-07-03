using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ini : MonoBehaviour {
    public ReadJson readJson;
    public CameraMovement_hemisphere cameraMovement_Hemisphere;

    public MeshVideo meshVideo;
    // Use this for initialization
    void Start () {
        StartCoroutine(initialization());

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator initialization() {
        yield return StartCoroutine(readJson.initialization());

        yield return StartCoroutine(cameraMovement_Hemisphere.initialization());

        meshVideo.initialization();
    }
}
