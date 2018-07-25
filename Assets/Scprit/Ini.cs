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
    // Use this for initialization
    void Start () {
        StartCoroutine(initialization());

	}
	

   public IEnumerator initialization() {
        readJson = FindObjectOfType<ReadJson>();

        cameraMovement_Hemisphere = FindObjectOfType<CameraMovement_hemisphere>();

        sendUPDData = FindObjectOfType<SendUPDData>();

        getUDPMessage = FindObjectOfType<GetUDPMessage>();

        meshVideo = FindObjectOfType<MeshVideo>();

        canvasCtr = FindObjectOfType<CanvasCtr>();

        yield return StartCoroutine(readJson.initialization());

        yield return StartCoroutine(cameraMovement_Hemisphere.initialization());

        meshVideo.initialization();

        getUDPMessage.InitializationUdp();

        sendUPDData.initialization();

        canvasCtr.initialization();
    }
}
