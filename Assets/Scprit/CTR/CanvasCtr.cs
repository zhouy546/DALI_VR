using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasCtr : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler {
    public static CanvasCtr instance;

    public VideoLobbyCtr videoLobbyCtr;
    public MenuCtr menuCtr;
    public GameObject console;
    public GameObject Menu;
    bool isConsoleOn;

    private bool doingOnce;
   public bool isMenuOn;

    NImage nImage;

    public void initialization() {
        if (instance == null) {
            instance = this;
        } 

        StartCoroutine(videoLobbyCtr.Initialization());
        menuCtr.initialization();

        menuCtr.InteractionToggle(isMenuOn);
        menuCtr.HideAll();

    }


    public void OnPointerDown(PointerEventData eventData)
    {
        //if (isMenuOn) {
        //    GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;

        //    videoFrameDoing(gameObject);
        //}

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //if (isMenuOn) {
        //    GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;

        //    voideFrameHeight(gameObject);
        //}
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //if (isMenuOn) {
        //    videoFrameDeHighlight();
        //}
    }

    void videoFrameDoing(GameObject gameObject) {

        //if (gameObject.GetComponent<NImage>() != null)
        //{
        //    NImage nImage = gameObject.GetComponent<NImage>();

        //    nImage.clickEvent.PlayVideo();
        //}
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
    void Update() {
        //-----------------------------------UDP----------------------------
        if (!isMenuOn) {
            //打开菜单
            if (ValueSheet.ExitTrigger || ValueSheet.EnterTrigger)
            {
                isMenuOn = !isMenuOn;

                menuCtr.ShowAll();

             //   PostProcessingCtr.instance.Blur();

                videoLobbyCtr.LookingForCurrentPlay();
            }
        }





        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isConsoleOn = !isConsoleOn;
            console.SetActive(isConsoleOn);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {

            if (!doingOnce) {
               StartCoroutine(resetdoingOnce());
                isMenuOn = !isMenuOn;
                menuCtr.InteractionToggle(isMenuOn);

                if (isMenuOn)
                {
                    menuCtr.ShowAll();
                    // Menu.GetComponent<NImage>().ShowAll();
                  PostProcessingCtr.instance.Blur();

                    videoLobbyCtr.LookingForCurrentPlay();
                }
                else
                {
                    videoLobbyCtr.PlayVideo();
                    //Menu.GetComponent<NImage>().HideAll();
                    menuCtr.HideAll();
                   PostProcessingCtr.instance.Focus();
                }
            }


        }


    }

    IEnumerator resetdoingOnce() {
        doingOnce = true;
        yield return new WaitForSeconds(1f);
        doingOnce = false;
    }
}
