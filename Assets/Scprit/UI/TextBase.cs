using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent (typeof (Text))]
public class TextBase : IRect {
    public Text text;

    public string currentTextContent, StartTextContent;

    public Color currentColor, DefaultColor, TargetColor;

    public List<TextBase> ChildrenTextBases;

    private LTDescr ColorLTDescr;


    public  new virtual void initialization()
    {
        base.initialization();
        text = this.GetComponent<Text>();
        ChildrenTextBases = GetChindrenText();
        currentColor = DefaultColor = text.color;
        currentTextContent = StartTextContent = text.text;
        startLocalPosition = currentLocalPosition = text.transform.localPosition;
    }

    public void ChangeColor(Color color, float time, LeanTweenType leanTweenType = LeanTweenType.notUsed, Action onColorChangeComplete = null)
    {
        if (ColorLTDescr != null)
        {
            CancelColorLeanTween();
        }

        if (currentColor == color)
        {
            return;
        }

        rect = GetComponent<RectTransform>();

        ColorLTDescr = LeanTween.value(0, 1, time).setEase(leanTweenType).setOnUpdate(delegate (float value)
        {
            float r = Mapping(value, 0, 1, currentColor.r, color.r);
            float g = Mapping(value, 0, 1, currentColor.g, color.g);
            float b = Mapping(value, 0, 1, currentColor.b, color.b);
            float a = Mapping(value, 0, 1, currentColor.a, color.a);
            currentColor = text.color = new Color(r, g, b, a);

        }).setOnComplete(delegate ()
        {
            currentColor = color;
            if (onColorChangeComplete != null) {
                onColorChangeComplete();
            }
            ColorLTDescr = null;
        });
    }

    protected virtual void DoSomethingAfterColorChange()
    {

    }

    protected void ChangeAlpha(float alpha, float time, LeanTweenType leanTweenType = LeanTweenType.notUsed, Action onAlphaChangeComplete = null)
    {
        Color tempColor = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
        ChangeColor(tempColor, time, leanTweenType, onAlphaChangeComplete);

    }

    protected virtual void DoSomethingAfterAlphaChange()
    {

    }

    protected void HideThisText(float time)
    {
        ChangeAlpha(0, time);
    }

    protected void ShowThisText(float time)
    {
        ChangeAlpha(1f, time);
    }

    public void HideAll(float time = 1)
    {
        HideThisText(time);
        HideChildren(time);
    }

    public void ShowAll(float time = 1)
    {
        ShowThisText(time);
        ShowChildren(time);
    }

    protected void HideChildren(float time)
    {
        foreach (var item in ChildrenTextBases)
        {
            item.HideThisText(time);
        }
    }

    protected void ShowChildren(float time)
    {
        foreach (var item in ChildrenTextBases)
        {
            item.ShowThisText(time);
        }
    }

    public void SetFrontTextSize(int size) {
        text.fontSize = size;
    }

    public string GetText() {
        return text.text;
    }

    public void SetText(string _text) {
        text.text = currentTextContent = _text;
    }

    protected List<TextBase> GetChindrenText()
    {
        return GetComponentsInChildren<TextBase>().ToList();
    }

    protected void CancelThisObjLeanTween()
    {
        CancelColorLeanTween();
        CancelMovementTween();
        CancelScalingTween();
    }

    private void CancelColorLeanTween()
    {
        LeanTween.cancel(ColorLTDescr.id);
    }
}
