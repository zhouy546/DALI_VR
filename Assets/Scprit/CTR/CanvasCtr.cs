using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasCtr : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler {
    public VideoLobbyCtr videoLobbyCtr;
    public MenuCtr menuCtr;
    public GameObject console;
    public GameObject Menu;
    bool isConsoleOn;
    bool isMenuOn;

    NImage nImage;

    public void initialization() {

        videoLobbyCtr.Initialization();
        menuCtr.initialization();

        menuCtr.InteractionToggle(isMenuOn);
        menuCtr.HideAll();

    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (isMenuOn) {
            GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;

            videoFrameDoing(gameObject);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isMenuOn) {
            GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;

            voideFrameHeight(gameObject);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isMenuOn) {
            videoFrameDeHighlight();
        }
    }

    void videoFrameDoing(GameObject gameObject) {

        if (gameObject.GetComponent<NImage>() != null)
        {
            NImage nImage = gameObject.GetComponent<NImage>();

            nImage.clickEvent.PlayVideo();
        }
    }


    void voideFrameHeight(GameObject gameObject)
    {
        // ----------------------------Highlight btn
        if (gameObject.GetComponent<NImage>() != null)
        {
            nImage = gameObject.GetComponent<NImage>();

            videoLobbyCtr.Heighlight(nImage);
        }
    }

    void videoFrameDeHighlight() {
        if (nImage != null)
        {
            //Debug.Log("exit");
            videoLobbyCtr.DeHeighlight(nImage);
        }
        nImage = null;
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isConsoleOn = !isConsoleOn;
            console.SetActive(isConsoleOn);
        }
        else if(Input.GetKeyDown(KeyCode.Space)) {
            isMenuOn = !isMenuOn;

            menuCtr.InteractionToggle(isMenuOn);

            if (isMenuOn)
            {
                menuCtr.ShowAll();
              //  Menu.GetComponent<NImage>().ShowAll();
                PostProcessingCtr.instance.Blur();
            }
            else {
                //Menu.GetComponent<NImage>().HideAll();
                menuCtr.HideAll();
                PostProcessingCtr.instance.Focus();
            }
        }


	}
}
