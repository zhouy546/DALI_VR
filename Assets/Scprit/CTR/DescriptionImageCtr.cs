using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionImageCtr : MonoBehaviour {
    public NImage nImage00, nImage01;

    void Start () {

	}
    public  void initialization() {

  

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

    int temp;
    IEnumerator goDescription() {

        temp = 0;
        nImage00.HideAll(0);
        nImage01.HideAll(0);

        List<Sprite> tempSprite = Ini.instance.infos[ValueSheet.CurrentPlayID].DescriptionImage;
        List<float> tempWaitTime = Ini.instance.infos[ValueSheet.CurrentPlayID].DescriptionImageTime;

     int length = tempSprite.Count;
       
       
        for (int i = 0; i < length; i++)
        {
            Debug.Log("i =" + i.ToString());
            yield return StartCoroutine(showDesImage(length, tempWaitTime, tempSprite, i));
            Debug.Log("goin next");
        }

        temp = 0;
    }


    IEnumerator showDesImage(int length, List<float> tempWaitTime, List<Sprite> tempSprite,int i) {
        if (length <= 1)
        {
            yield return new WaitForSeconds(tempWaitTime[temp]);
            nImage00.image.sprite = tempSprite[i];
            nImage00.ShowAll(5);
        }
        else
        {
            //play the sprite list
            if (temp < tempSprite.Count)
            {
                yield return new WaitForSeconds(tempWaitTime[temp]);//从JSON过来的一个ARRAY[TEMP]调节出现间隔    
                nImage00.image.sprite = tempSprite[temp];
                nImage00.ShowAll(5);
                nImage01.HideAll(5);
                if (temp+1 < tempSprite.Count) {
                    temp++;
                    yield return new WaitForSeconds(tempWaitTime[temp]);
                    nImage00.HideAll(5);
                    nImage01.image.sprite = tempSprite[temp];
                    nImage01.ShowAll(5);
                    temp++;
                }
            }
        }

    }


}
