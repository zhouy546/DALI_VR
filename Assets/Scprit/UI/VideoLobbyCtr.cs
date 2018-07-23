using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VideoLobbyCtr : MonoBehaviour {
    bool isAwake = true;
    public GameObject VideoFrame;
    public List<NImage> VideoFrameNImages = new List<NImage>();
    public List<NImage> AllNImage = new List<NImage>();
    public List<Ntext> AllNText = new List<Ntext>();

    [System.Serializable]
    public class SlotHolder {
        public Vector3 SlotPos;
        public Vector3 SlotScale;
        public NImage nImage;

        public SlotHolder() {

        }
        public SlotHolder(Vector3 _slotPos,Vector3 _slotScale)
        {
            SlotPos = _slotPos;
            SlotScale = _slotScale;
        }

    }


    [System.Serializable]
    public class DisplayHolder
    {
        public Vector3 SlotPos;
        public Vector3 SlotScale;
        public DisplayHolder()
        {

        }
        public DisplayHolder(Vector3 _slotPos, Vector3 _slotScale)
        {
            SlotPos = _slotPos;
            SlotScale = _slotScale;
        }

    }

    public int DisplaySlotNum;
    public List<DisplayHolder> DisplaySlot= new List<DisplayHolder>();
    public List<SlotHolder> slotHolder= new List<SlotHolder>();
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            JoyStickMoveLeft();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            JoyStickMoveRight();
    }

    public void Initialization() {

        if (isAwake) {
            for (int i = 0; i < DisplaySlotNum+2; i++)
            {
                GameObject _gameObject = Instantiate(VideoFrame);

                _gameObject.transform.parent = this.transform;

                _gameObject.transform.localPosition = Vector3.zero;

                NImage image = _gameObject.GetComponent<NImage>();

                image.initialization();

                _gameObject.name = i.ToString();
                    //= image.clickEvent.path = ValueSheet.videoPath[i]; ;

                VideoFrameNImages.Add(image);          

            }
            //----------------
            GenerateSlot();
            //------------------
            isAwake = !isAwake;
        }
    }

    void GenerateSlot() {


        for (int i = 0; i < DisplaySlotNum + 2; i++) { 

            float xPos = Mapping(i, 0, DisplaySlotNum +2- 1, -800f, 800f);

            float scale = Mathf.Sin(Mathf.Deg2Rad * Mapping(i, 0, DisplaySlotNum +2- 1, 0, 180));

            slotHolder.Add(new SlotHolder(new Vector3(xPos, 0, 0), new Vector3(scale, scale, scale)));

            slotHolder[i].nImage = VideoFrameNImages[i];
        }

        foreach (var item in slotHolder)
        {
            item.nImage.SetLocation(item.SlotPos,0);
            item.nImage.SetScale(item.SlotScale, 0);

        }


    }

    public void JoyStickMoveLeft() {


        //NImage temp = VideoFrameNImages[VideoFrameNImages.Count-1];

        //for (int i = 0; i < ValueSheet.videoPath.Length; i++)
        //{
        //    int Num = GetLeftNum(i);
        //    VideoFrameNImages[i].SetLocation(slotHolder[Num].SlotPos, .5f);
        //    VideoFrameNImages[i].SetScale(slotHolder[Num].SlotScale,.5f);
        //}

        //for (int i = VideoFrameNImages.Count-1; i >= 0; i--)
        //{
        //    if (i != 0)
        //    {
        //        VideoFrameNImages[i] = VideoFrameNImages[i - 1];

        //    }
        //    else if(i==0)
        //    {
        //        VideoFrameNImages[i] = temp;
        //    }
        //}
    }

    public void JoyStickMoveRight() {
        NImage temp = VideoFrameNImages[0];

        for (int i = 0; i < ValueSheet.videoPath.Length; i++)
        {
            int Num = GetRightNum(i);
            VideoFrameNImages[i].SetLocation(slotHolder[Num].SlotPos, .5f);
            VideoFrameNImages[i].SetScale(slotHolder[Num].SlotScale, .5f);

            if (i != ValueSheet.videoPath.Length - 1)
            {
                VideoFrameNImages[i] = VideoFrameNImages[i + 1];
            }
            else {
                VideoFrameNImages[i] = temp;
            }
        }
    }

    int GetLeftNum(int value) {
        int temp = value-1;

        if (temp < 0)
        {
            temp = ValueSheet.videoPath.Length - 1;

        }
        return temp;
    }

    int GetRightNum(int value)
    {
        int temp = value + 1;

        if (temp >ValueSheet.videoPath.Length - 1)
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

    float Mapping(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {

        float outVal = ((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin);

        return outVal;
    }
}
