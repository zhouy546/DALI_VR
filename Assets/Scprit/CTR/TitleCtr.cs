using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleCtr : MonoBehaviour {
    List<Sprite> TitleSprites = new List<Sprite>();

    NImage LeftImage;
    NImage RightImage;
    NImage TitleImage;

    public string path;

	// Use this for initialization
	void Start () {
		
	}

    public void initialization() {
        TitleSprites = getTitleImageList();
    }

    public List<Sprite> getTitleImageList() {
        List<Sprite> sprites = new List<Sprite>();

        return sprites;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void ShowTitle() {

    }

    void HideTitle() {

    }
}
