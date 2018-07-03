using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderBase : IRect, IPointerDownHandler,IPointerUpHandler
{
    public bool IsOnClicked;

    protected Slider slider;

    public new virtual void initialization() {
        base.initialization();

        slider = this.GetComponent<Slider>();

    }

    public void SetValue(float value) {
        slider.value = value;
    }

   public float  GetValue() {
        return slider.value;
    }


    public virtual void OnPointerDown(PointerEventData eventData)
    {

    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {

    }

    public virtual void OnValueChange() {
    
    }
}
