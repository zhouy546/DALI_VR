using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionImageSystem : MonoBehaviour {
    public Transform Locator00Trans, Locator01Trans;
    public Transform TargetLocationPos, StartLocationPos;

    SpriteRenderer Locator00Sprite, Locator01Sprite;
    // Use this for initialization
    void Start () {
        Locator00Sprite = Locator00Trans.GetComponent<SpriteRenderer>();
        Locator01Sprite = Locator01Trans.GetComponent<SpriteRenderer>();

        Locator00Trans.localScale = Locator01Trans.localScale = Vector3.zero;

        HideAll();



    }

    public void HideAll(float time = 1) {
        StopAllCoroutines();
        ChangeAlpha(Locator00Sprite, 255, 0, time);
        ChangeAlpha(Locator01Sprite, 255, 0, time);
        temp = 0;
        Locator00Trans.SetParent(StartLocationPos);
        Locator01Trans.SetParent(StartLocationPos);
        Locator00Trans.localPosition = Locator01Trans.localPosition = Vector3.zero;
        Locator00Trans.localScale = Locator01Trans.localScale = Vector3.zero;
    }

    void Hide(SpriteRenderer sprite) {
        ChangeAlpha(sprite, 1, 0, 1f,()=> ResetObjPos(sprite.transform));
    }

    void ResetObjPos(Transform _transform) {
        _transform.localPosition =Vector3.zero;
        _transform.localScale = Vector3.zero;
        _transform.localRotation=  Quaternion.Euler(Vector3.zero);
    }

    void Show(SpriteRenderer sprite)
    {
        ChangeAlpha(sprite, 0, 1, 1f);
    }


    void TheShowAnimation(Transform Locator) {

        Vector3 vector3 = new Vector3(.1f, .1f, .1f);
        ScaleUp(Locator,vector3 , LeanTweenType.notUsed, .6f, ()=> MoveAndScale(Locator));
    }

    void MoveAndScale(Transform Locator) {
        Vector3 vector3 = new Vector3(.5f, .5f, .5f);
        Move(Locator, Vector3.zero, LeanTweenType.easeInOutSine);
        ScaleUp(Locator, vector3, LeanTweenType.easeInOutSine);
    }

    void ScaleUp(Transform Locator,Vector3 scale, LeanTweenType leanTweenType = LeanTweenType.notUsed, float time=.5f,Action action = null) {
       

        LeanTween.scale(Locator.gameObject, scale, time).setOnComplete(delegate() {
            if (action != null) {
                action();
            }

        }).setEase(leanTweenType);
    }

    void ScaleDown(Transform Locator) {
        LeanTween.scale(Locator.gameObject, Vector3.zero, .5f);
    }

    private void Move(Transform Locator, Vector3 target, LeanTweenType leanTweenType = LeanTweenType.notUsed, Action action = null)
    {
        LeanTween.moveLocal(Locator.gameObject, target, .5f).setOnComplete(delegate () {
            if (action != null)
            {
                action();
            }
        }).setEase(leanTweenType);
    }


    void ChangeRotation(Transform Locator, Vector3 target) {

        Vector3 perviousRot = Locator.localRotation.eulerAngles;
        Vector3 ROT;

        float X;
        float Y;
        float Z;

        LeanTween.value(0, 1, 0.5f).setOnUpdate(delegate (float val) {
            //  ROT = perviousRot + (target - perviousRot) * val;
            // Debug.Log(val);
            X = easeAngle(perviousRot.x, target.x, val);
            Y = easeAngle(perviousRot.y, target.y, val);
            Z = easeAngle(perviousRot.z, target.z, val);
            ROT = new Vector3(X, Y, Z);
            Locator.localRotation = Quaternion.Euler(ROT);
        });
    }

    float easeAngle(float from, float to, float per) {
        float x;
        if (360 - from < 180)
        {
            x = from + (to - from + 360) * per;
        }
        else
        {
            x = from + (to - from) * per;
        }

        return x;
    }

    void ChangeAlpha(SpriteRenderer sprite,float from, float to,float time,Action action=null) {

        LeanTween.value(from, to, time).setOnUpdate(delegate (float value)
        {
            float r = sprite.color.r;
            float g = sprite.color.g;
            float b = sprite.color.b;
            float a = value;

            sprite.color = new Color(r, g, b, a);
        }).setOnComplete(delegate () {
            if (action != null) {
                action();
            }
        });
    }

    

	// Update is called once per frame



    public void ShowDescription()
    {
        StopAllCoroutines();
        StartCoroutine(DescriptionGO());
    }

    int temp;
    IEnumerator DescriptionGO() {
        int temp = 0;
        ChangeAlpha(Locator00Sprite, 255, 0, 0);
        ChangeAlpha(Locator01Sprite, 255, 0, 0);

        List<Sprite> tempSprite = Ini.instance.infos[ValueSheet.CurrentPlayID].DescriptionImage;
        List<float> tempWaitTime = Ini.instance.infos[ValueSheet.CurrentPlayID].DescriptionImageTime;
        List<Vector3> tempPos = Ini.instance.infos[ValueSheet.CurrentPlayID].DescriptionImagePos;

        //foreach (var item in tempSprite)
        //{
        //    Debug.Log(item.name);
        //}

        int length = tempSprite.Count;

        for (int i = 0; i < length; i++)
        {
            Debug.Log("i =" + i.ToString());
            yield return StartCoroutine(showDesImage(length, tempWaitTime, tempPos, tempSprite, i));
           Debug.Log("goin next");
        }
        temp = 0;


    }


    IEnumerator showDesImage(int length, List<float> tempWaitTime, List<Vector3> tempPos, List<Sprite> tempSprite, int i)
    {
        
        if (length <= 1)
        {
            yield return new WaitForSeconds(tempWaitTime[temp]);
            FadeIn(Locator00Sprite, Locator00Trans, i, tempPos, tempSprite);

          StartCoroutine(FadeOut(Locator00Sprite, Locator00Trans,5f));
        }
        else {
            if (temp < tempSprite.Count) {
                Debug.Log("temp : " + temp);
                yield return new WaitForSeconds(tempWaitTime[temp]);               

                FadeIn(Locator00Sprite, Locator00Trans, temp, tempPos, tempSprite);

                StartCoroutine(FadeOut(Locator00Sprite, Locator00Trans, 5f));

                if (temp + 1 < tempSprite.Count) {
                    temp++;
                    Debug.Log("temp : " + temp);
                    yield return new WaitForSeconds(tempWaitTime[temp]);

                    FadeIn(Locator01Sprite, Locator01Trans, temp, tempPos, tempSprite);

                    StartCoroutine(FadeOut(Locator01Sprite, Locator01Trans, 5f));

                    temp++;
                }
            }
        }

    }


    public void FadeIn(SpriteRenderer Render,Transform Locator, int i, List<Vector3> tempPos, List<Sprite> tempSprite) {
       // Debug.Log("i: "+i+" "+tempSprite[i].name);
            
        //setImage
        Render.sprite = tempSprite[i];
        //ALPHA TO 1
        Show(Render);
        //SetStartPos
        Locator.localPosition = tempPos[i];

        Locator.SetParent(TargetLocationPos);
        // scale up,
        //ScaleUp(Locator);
        //move;
        TheShowAnimation(Locator);

        //rotation
        ChangeRotation(Locator, Vector3.zero);
    }


    IEnumerator FadeOut(SpriteRenderer Render, Transform Locator,float waitTime=0) {
        yield return new WaitForSeconds(waitTime);

        //AlphaTOZero
        Hide(Render);

        Locator.SetParent(StartLocationPos);

        // scale down,
        //ScaleDown(Locator);

        //set location to 00
        //Move(Locator, Vector3.zero);
    }

   
}
