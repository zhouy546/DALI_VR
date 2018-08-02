using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionImageCtr : MonoBehaviour {
    public NImage nImage00, nImage01;

    void Start () {

	}

    private void OnEnable()
    {
        if (MeshVideo.instance != null) {
            MeshVideo.instance.VideoPlayEvent += ShowDescription;
        }
    }

    public void OnDisable()
    {
        MeshVideo.instance.VideoPlayEvent -= ShowDescription;
    }

    public  void initialization() {

        MeshVideo.instance.VideoPlayEvent += ShowDescription;

    }

    // Update is called once per frame
    void Update () {
		
	}


    public void ShowDescription() {
        StopAllCoroutines();
        StartCoroutine(goDescription());
    }

    public void HideAll() {
        StopAllCoroutines();
        nImage00.HideAll();
        nImage01.HideAll();

    }


    IEnumerator goDescription() {

        nImage00.HideAll(0);
        nImage01.HideAll(0);

        List<Sprite> tempSprite = Ini.instance.infos[ValueSheet.CurrentPlayID].DescriptionImage;

     int length = tempSprite.Count;
       
        int temp = 0;
        for (int i = 0; i < length; i++)
        {
            if (length <= 1)
            {
                yield return new WaitForSeconds(5);
                nImage00.image.sprite = tempSprite[i];
                nImage00.ShowAll(5);
            }
            else {
                //play the sprite list
                if (temp < tempSprite.Count) {//检查temp++时最后一个不能超出界限
                    yield return new WaitForSeconds(5);//从JSON过来的一个ARRAY[TEMP]调节出现间隔      
                    nImage00.image.sprite = tempSprite[temp];
                    nImage00.ShowAll(5);
                    nImage01.HideAll(5);
                    yield return new WaitForSeconds(5);
                    nImage00.HideAll(5);
                    temp++;
                    nImage01.image.sprite = tempSprite[temp];
                    nImage01.ShowAll(5);
                    temp++;
                }
            }
          
        }
    }


}
