using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoLobbyCtr : MonoBehaviour {
    bool isAwake = true;
    public GameObject VideoFrame;
    public List<NImage> nImages = new List<NImage>();
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

                _gameObject.name= image.clickEvent.path = ValueSheet.videoPath[i]; ;

                nImages.Add(image);
            }

            isAwake = !isAwake;
        }


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
        foreach (var item in nImages)
        {
            if (item==_nImage)
            {
                return item;
            }
        }

        return null;
    }
}
