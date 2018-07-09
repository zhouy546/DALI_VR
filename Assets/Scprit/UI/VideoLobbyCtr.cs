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
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialization() {

        if (isAwake) {
            for (int i = 0; i < ValueSheet.videoPath.Length; i++)
            {
                GameObject _gameObject = Instantiate(VideoFrame);

                _gameObject.transform.parent = this.transform;

                NImage image = _gameObject.GetComponent<NImage>();

                image.initialization();

                _gameObject.name= image.clickEvent.path = ValueSheet.videoPath[i]; ;

                VideoFrameNImages.Add(image);
            }

            isAwake = !isAwake;
        }


        //Debug.Log("AllNimages NUM"+ AllNimages.Count);
        //foreach (var item in AllNimages)
        //{
        //    Debug.Log("Name" + item.name);
        //}
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

}
