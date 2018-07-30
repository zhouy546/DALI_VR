using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ini : MonoBehaviour {
    private ReadJson readJson;

    private CameraMovement_hemisphere cameraMovement_Hemisphere;

    private GetUDPMessage getUDPMessage;

    private SendUPDData sendUPDData;

    private MeshVideo meshVideo;

    private CanvasCtr canvasCtr;

    private MapAnimation mapAnimation;
    // Use this for initialization
    void Awake () {
        StartCoroutine(initialization());

	}
	

   public IEnumerator initialization() {
        Cursor.visible = false;

        Screen.SetResolution(1920, 1200, false);

        readJson = FindObjectOfType<ReadJson>();

        cameraMovement_Hemisphere = FindObjectOfType<CameraMovement_hemisphere>();

        sendUPDData = FindObjectOfType<SendUPDData>();

        getUDPMessage = FindObjectOfType<GetUDPMessage>();

        meshVideo = FindObjectOfType<MeshVideo>();

        mapAnimation = FindObjectOfType<MapAnimation>();

        canvasCtr = FindObjectOfType<CanvasCtr>();

        yield return StartCoroutine(readJson.initialization());

        yield return StartCoroutine(cameraMovement_Hemisphere.initialization());

        meshVideo.initialization();

        getUDPMessage.InitializationUdp();

        sendUPDData.initialization();

        canvasCtr.initialization();

        mapAnimation.initialization();

    }
}
