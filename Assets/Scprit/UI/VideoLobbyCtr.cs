using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using UnityEngine;

public class VideoLobbyCtr : MonoBehaviour {
    bool isAwake = true;
    public GameObject VideoFrame;

    public List<NImage> AllNImage = new List<NImage>();
    public List<Ntext> AllNText = new List<Ntext>();

    public List<Sprite> SpritesListData = new List<Sprite>();

    public List<NImage> VideoFrameNImages = new List<NImage>();

    public List<ScrollSpritesInfo> ScrollSpritesList = new List<ScrollSpritesInfo>();


    private bool doingOnce;

    public class ScrollSpritesInfo {
        public Sprite sprite;
        public string path;

        public ScrollSpritesInfo() {

        }

        public ScrollSpritesInfo(Sprite _sprite, string _path) {
            sprite = _sprite;
            path = _path;
        }
    }

    [System.Serializable]
    public class SlotHolder {
        public Vector3 SlotPos;
        public Vector3 SlotScale;

        public SlotHolder() {

        }
        public SlotHolder(Vector3 _slotPos,Vector3 _slotScale)
        {
            SlotPos = _slotPos;
            SlotScale = _slotScale;
        }

    }

    public int DisplaySlotNum;

    public List<SlotHolder> slotHolder= new List<SlotHolder>();

    public  CanvasCtr canvasCtr;

    public VideoFrameClickEvent currentSelected;

    private int currentVideoNum;


    void Start () {

    }

    void LoadSeq()
    {       
        for (int i = 0; i < ValueSheet.videoPath.Length; i++)
        {
            SpritesListData.Add(Ini.instance.infos[i].MenuImage);

        }

        int RawLenth = ScrollSpritesList.Count;

        while (ScrollSpritesList.Count< VideoFrameNImages.Count)
        {
            foreach (var item in SpritesListData)
            {
                ScrollSpritesInfo scrollSpritesInfo = new ScrollSpritesInfo(item, "");
                ScrollSpritesList.Add(scrollSpritesInfo);
            }
        }

        int num = 0;
        foreach (var item in VideoFrameNImages)
        {
            item.image.sprite = ScrollSpritesList[num].sprite;
                 num++;
        }
    }
    // Update is called once per frame
    float tempzAxis;
    float currentZAxis;
    float RotCoolDownTime;


    void Update () {
        if (canvasCtr.isMenuOn)
        {

            float zAxis = -Input.GetAxis("Mouse X");
            if (tempzAxis == 0 && zAxis == 0)
            {
                currentZAxis = 0;
            }
            else if (zAxis == 0 && tempzAxis != 0)
            {
                currentZAxis = Mathf.Clamp(tempzAxis, -1.5f, 1.5f);
            }
            else if (zAxis != 0 && tempzAxis != 0)
            {
                currentZAxis = Mathf.Clamp(zAxis, -1.5f, 1.5f);
            }

            if (zAxis < -0.5)
            {
                if (!doingOnce)
                {
                    StartCoroutine(resetdoingOnce());
                    JoyStickMoveLeft();
                }
            }
            else if (zAxis > 0.5) {
                if (!doingOnce)
                {
                    StartCoroutine(resetdoingOnce());
                    JoyStickMoveRight();
                }
            }

            tempzAxis = zAxis;

            //if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            //    if (!doingOnce)
            //    {
            //        StartCoroutine(resetdoingOnce());
            //        JoyStickMoveLeft();
            //    }
            //}

            //if (Input.GetKeyDown(KeyCode.RightArrow)) {
            //    if (!doingOnce)
            //    {
            //        StartCoroutine(resetdoingOnce());
            //        JoyStickMoveRight();
            //    }

            //}

            //if (Input.GetKeyDown(KeyCode.A)) {
            //    PlayVideo();
            //}
        }


    }

    public void Initialization() {
        if (isAwake) {
            for (int i = 0; i < DisplaySlotNum+2; i++)
            {
                GameObject _gameObject = Instantiate(VideoFrame);

                _gameObject.transform.SetParent(this.transform);

                _gameObject.transform.localPosition = Vector3.zero;

                NImage image = _gameObject.GetComponent<NImage>();

                image.initialization();

                _gameObject.name = i.ToString();

               // image.clickEvent.path = ValueSheet.videoPath[i]; ;

                VideoFrameNImages.Add(image);          

            }
            //----------------
            GenerateSlot();

            LoadSeq();


            int value = VideoFrameNImages.Count / 2;

            UpdateSelectSlot(value);
            //------------------
            isAwake = !isAwake;
        }

    }

    void GenerateSlot() {

        for (int i = 0; i < DisplaySlotNum + 2; i++) { 

            float xPos = UtilityFunction.Mapping(i, 0, DisplaySlotNum +2- 1, -800f, 800f);

            float scale = Mathf.Sin(Mathf.Deg2Rad * UtilityFunction.Mapping(i, 0, DisplaySlotNum +2- 1, 0, 180));

            slotHolder.Add(new SlotHolder(new Vector3(xPos, 0, 0), new Vector3(scale, scale, scale)));
        
        }

        int value = 0;
        foreach (var item in VideoFrameNImages)
        {
            item.SetLocation(slotHolder[value].SlotPos, 0);
            item.SetScale(slotHolder[value].SlotScale, 0);
            value++;
        }
    }

    private void UpdateUIImage(int value) {

        VideoFrameNImages[0].image.sprite = ScrollSpritesList[0].sprite;       

        VideoFrameNImages[VideoFrameNImages.Count - 1].image.sprite = ScrollSpritesList[VideoFrameNImages.Count - 1].sprite;

        UpdateSelectSlot(value);
    }

    private void UpdateSelectSlot(int value) {
        int num = VideoFrameNImages.Count / 2;

        if (currentSelected != null)
        {
            List<VideoFrameClickEvent> videoFrameClickEvents = new List<VideoFrameClickEvent>();

            for (int i = 0; i < VideoFrameNImages.Count; i++)
            {
                videoFrameClickEvents.Add(VideoFrameNImages[i].GetComponent<VideoFrameClickEvent>());
            }


            for (int i = 0; i < videoFrameClickEvents.Count; i++)
            {
                if (i < num)
                {
                    videoFrameClickEvents[i].SetLayer(i);
                }
                else
                {
                    videoFrameClickEvents[i].SetLayer(num * 2 - i);
                }
            }
        }
     

        currentVideoNum = value;

        if (currentSelected != null){
           // DeHeighlight(currentSelected.GetComponent<NImage>());
            currentSelected.SetLayer(2);
        }

        currentSelected = VideoFrameNImages[num].GetComponent<VideoFrameClickEvent>();
        if (!isAwake) {
            //Heighlight(VideoFrameNImages[num]);
            currentSelected.SetLayer(5);

        }

        currentSelected.path = ValueSheet.videoPath[value];

        currentSelected.id = value;
    }


    public void LookingForCurrentPlay() {
        while (("Video/"+currentSelected.path)!= MeshVideo.instance.path)
        {
            JoyStickMoveLeft();
        }
    }

    public void JoyStickMoveLeft() {

        UIMoveLeft();
    }

    public void JoyStickMoveRight()
    {
        UIMoveRight();
    }

    public void PlayVideo() {

        ValueSheet.CurrentPlayID = currentSelected.id;

        currentSelected.PlayVideo();
    }

    void UIMoveLeft() {
        NImage temp = VideoFrameNImages[VideoFrameNImages.Count - 1];

        ScrollSpritesInfo spriteTemp = ScrollSpritesList[ScrollSpritesList.Count - 1];

        for (int i = 0; i < VideoFrameNImages.Count; i++)
        {
            int Num = GetRightNum(i, slotHolder.Count);

            VideoFrameNImages[i].SetLocation(slotHolder[Num].SlotPos, .2f);

            VideoFrameNImages[i].SetScale(slotHolder[Num].SlotScale, .2f);
        }

        
        UpdateListLeft<NImage>(VideoFrameNImages.Count, VideoFrameNImages, temp);

        UpdateListLeft<ScrollSpritesInfo>(ScrollSpritesList.Count, ScrollSpritesList, spriteTemp);

        UpdateUIImage(GetLeftNum(currentVideoNum,ValueSheet.videoPath.Length));
    }

    void UpdateListLeft<T>(int Length, List<T> list, T temp)
    {
        for (int i = Length - 1; i >= 0; i--)
        {
            if (i != 0)
            {
                list[i] = list[i - 1];
            }
            else if (i == 0)
            {
                list[i] = temp;
            }
        }
    }

    void UIMoveRight() {
        NImage temp = VideoFrameNImages[0];
        ScrollSpritesInfo spriteTemp = ScrollSpritesList[0];

        for (int i = 0; i < VideoFrameNImages.Count; i++)
        {
            int Num = GetLeftNum(i, slotHolder.Count);

            VideoFrameNImages[i].SetLocation(slotHolder[Num].SlotPos, .2f);

            VideoFrameNImages[i].SetScale(slotHolder[Num].SlotScale, .2f);
        }

        UpdateListRight<NImage>(VideoFrameNImages.Count, VideoFrameNImages, temp);

        UpdateListRight<ScrollSpritesInfo>(ScrollSpritesList.Count, ScrollSpritesList, spriteTemp);

        UpdateUIImage(GetRightNum(currentVideoNum, ValueSheet.videoPath.Length));
    }

    void UpdateListRight<T>(int Length, List<T> list, T temp) {
        for (int i = 0; i < Length; i++)
        {
            if (i != Length - 1)
            {
                list[i] = list[i + 1];
            }
            else
            {
                list[i] = temp;
            }
        }
    }


    int GetLeftNum(int value,int length) {
        int temp = value-1;

        if (temp < 0)
        {
            temp = length - 1;

        }
        return temp;
    }

    int GetRightNum(int value, int length)
    {
        int temp = value + 1;

        if (temp > length - 1)
        {
            temp = 0;

        }
        return temp;
    }


    public void Clicked(NImage _nImage) {
        NImage image = searchNimage(_nImage);
        if (image != null)
        {
            image.clickEvent.PlayVideo();

        }

    }

    public void Heighlight(NImage _nImage) {
        NImage image = searchNimage(_nImage);

        if (image != null)
        {
            image.ChangeColor(Color.red, 1f);

        }
    }

    public void DeHeighlight(NImage _nImage) {

        NImage image = searchNimage(_nImage);

        if (image != null) {
            image.ChangeColor(Color.white, 1f);
        }

    }



    public NImage searchNimage(NImage _nImage) {
        foreach (var item in VideoFrameNImages)
        {
            if (item==_nImage)
            {
                return item;
            }
        }

        return null;
    }

    IEnumerator resetdoingOnce()
    {
        doingOnce = true;
        yield return new WaitForSeconds(1f);
        doingOnce = false;
    }

}
