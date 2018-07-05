using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRect : MonoBehaviour {
    public Vector3 startLocalPosition, currentLocalPosition;
    public Vector3 startScale, currentScale;
    public RectTransform rect;
    protected LTDescr MoveLTDescr, ScalingLTDescr;

    public void Start()
    {

    }

   public virtual   void  initialization()
    {
        rect = this.GetComponent<RectTransform>();
    }

    public void SetLocation(Vector3 pos, float time, LeanTweenType leanTweenType = LeanTweenType.notUsed,Action OnMoveComplete = null) {
        if (MoveLTDescr != null)
        {
            CancelMovementTween();
        }
        MoveLTDescr = LeanTween.moveLocal(this.gameObject, pos, time).setEase(leanTweenType).setOnUpdate(delegate(float value) {
            currentLocalPosition = rect.localPosition;
        }).setOnComplete(delegate() {
            currentLocalPosition = pos;

            if (OnMoveComplete != null) {
                OnMoveComplete();
            }


            MoveLTDescr = null;
        });
    }

    protected virtual void DoSomethingAfterMove()
    {

    }

    public void SetScale(Vector3 scale,float time,LeanTweenType leanTweenType=LeanTweenType.notUsed, Action OnScalingComplete = null) {
        if (ScalingLTDescr != null)
        {
            CancelScalingTween();
        }

        ScalingLTDescr = LeanTween.scale(this.gameObject, scale, time).setEase(leanTweenType).setOnUpdate(delegate (float value)
        {
            currentScale = rect.localScale;
        }).setOnComplete(delegate () {
            currentScale = scale;
            if (OnScalingComplete != null) {
                OnScalingComplete();
            }
            ScalingLTDescr = null;
        });
    }

    protected virtual void DoSomethingAfterScaling()
    {

    }

    protected void CancelMovementTween() {
        LeanTween.cancel(MoveLTDescr.id);
    }

    protected void CancelScalingTween()
    {
        LeanTween.cancel(ScalingLTDescr.id);
    }

    protected void Enable()
    {
        this.gameObject.SetActive(true);
    }

    protected void Disable() {
        this.gameObject.SetActive(false);
    }

    public float Mapping(float value, float inputMin, float inputMax, float outputMin, float outputMax) {
        float outVal = ((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin);
        return outVal;
    }
}
