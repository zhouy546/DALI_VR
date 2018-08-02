using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCtr : MonoBehaviour {
    public NImage LeftImage, RightImage, TitleImage;

    public float width {
        get { return Ini.instance.infos[ValueSheet.CurrentPlayID].TitleSprite.rect.width; }
    }

    public float height {
        get { return Ini.instance.infos[ValueSheet.CurrentPlayID].TitleSprite.rect.height; }
    }

    private float Startoffset = 100;

    private float MoveDis = 50;

    public float EndOffset {
        get { return Startoffset - MoveDis; }
    }

    public Vector3 LeftStartPos
    {
        get {
            float x = -(width / 2 + Startoffset);
            return new Vector3(x, LeftImage.transform.localPosition.y, LeftImage.transform.localPosition.z);
        }
    }

    public Vector3 LeftEndPos
    {
        get
        {
            float x = -(width / 2 + EndOffset);
            return new Vector3(x, LeftImage.transform.localPosition.y, LeftImage.transform.localPosition.z);
        }
    }

    public Vector3 RightStartPos
    {
        get
        {
            float x = (width / 2 + Startoffset);
            return new Vector3(x, RightImage.transform.localPosition.y, RightImage.transform.localPosition.z);
        }
    }

    public Vector3 RightEndPos
    {
        get
        {
            float x = (width / 2 + EndOffset);
            return new Vector3(x, RightImage.transform.localPosition.y, RightImage.transform.localPosition.z);
        }
    }


    public void OnEnable()
    {
        if (MeshVideo.instance != null) {
            MeshVideo.instance.VideoPlayEvent += ShowTitle;
        }
    }

    public void OnDisable()
    {
        MeshVideo.instance.VideoPlayEvent -= ShowTitle;
    }

    public void initialization() {
        MeshVideo.instance.VideoPlayEvent += ShowTitle;
    }

    public void ShowTitle() {
        Debug.Log("Show Me");
        TitleImage.image.sprite = Ini.instance.infos[ValueSheet.CurrentPlayID].TitleSprite;
        TitleImage.image.rectTransform.sizeDelta = new Vector2(width, height);
        ShowBrackets();

        LeftImage.ShowAll();
        RightImage.ShowAll();
        TitleImage.ShowAll();
    }


    void ShowBrackets() {
        showLeftBracket();
        showRightBracket();
    }

    void showLeftBracket() {
        LeftImage.SetLocation(LeftStartPos, 0);

        LeftImage.SetLocation(LeftEndPos, 1);
    }

    void showRightBracket()
    {

        RightImage.SetLocation(RightStartPos, 0);

        RightImage.SetLocation(RightEndPos, 1);
    }


    public void HideAll() {
        LeftImage.HideAll();
        RightImage.HideAll();
        TitleImage.HideAll();
    }

}