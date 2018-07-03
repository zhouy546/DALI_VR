using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class ImageBase : IRect {

    protected delegate void OnAlphaChangeComplete();

    public Color currentColor, DefaultColor,TargetColor;
    public Image image;

    public List<ImageBase> ChildrenimageBases;

    private LTDescr  ColorLTDescr;

    public new  virtual void initialization() {

        base.initialization();

        image = this.GetComponent<Image>();

        ChildrenimageBases = GetChindrenImg();

        currentColor = DefaultColor =image.color;

        startLocalPosition = currentLocalPosition = image.transform.localPosition;
    }

    public void ChangeColor(Color color, float time, LeanTweenType leanTweenType = LeanTweenType.notUsed, Action onColorChangeComplete = null) {
        if (ColorLTDescr != null)
        {
            CancelColorLeanTween();
        }

        if (currentColor == color) {
            return;
        }

         rect = GetComponent<RectTransform>();

        ColorLTDescr = LeanTween.value(0,1, time).setEase(leanTweenType).setOnUpdate(delegate (float value)
        {
          float r=  Mapping(value,0, 1, currentColor.r, color.r);
          float g = Mapping(value, 0, 1, currentColor.g, color.g);
          float b = Mapping(value, 0, 1, currentColor.b, color.b);
          float a = Mapping(value, 0, 1, currentColor.a, color.a);
          currentColor = image.color = new Color(r, g, b, a);

        }).setOnComplete(delegate ()
        {
            currentColor = color;
            if (onColorChangeComplete != null)
                onColorChangeComplete();
        });
    }
    protected virtual void DoSomethingAfterColorChange()
    {

    }

    protected void ChangeAlpha(float alpha,float time,LeanTweenType leanTweenType = LeanTweenType.notUsed, Action onAlphaChangeComplete = null) {
        Color tempColor =new  Color(currentColor.r, currentColor.g, currentColor.b, alpha);
        ChangeColor(tempColor, time, leanTweenType, onAlphaChangeComplete);

    }

    protected virtual void DoSomethingAfterAlphaChange() {

    }

    protected void HideThisImage(float time) {
        ChangeAlpha(0, time);
    }

    protected void ShowThisImage(float time ) {
        ChangeAlpha(1f,time);
    }

    public void HideAll(float time = 1) {
        HideThisImage(time);
        HideChildren(time);
    }

    public void ShowAll(float time = 1) {
        ShowThisImage(time);
       ShowChildren(time);
    }

    protected void HideChildren(float time) {
        foreach (var item in ChildrenimageBases)
        {
            item.HideThisImage(time);
        }
    }

    protected void ShowChildren(float time)
    {
        foreach (var item in ChildrenimageBases)
        {
            item.ShowThisImage(time);
        }
    }

    protected List<ImageBase> GetChindrenImg() {
      return  GetComponentsInChildren<ImageBase>().ToList();
    }

    protected void CancelThisObjLeanTween() {
        CancelColorLeanTween();
        CancelMovementTween();
        CancelScalingTween();
    }

    private void CancelColorLeanTween() {
        LeanTween.cancel(ColorLTDescr.id);
    }

}
