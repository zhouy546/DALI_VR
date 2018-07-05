using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleMouseCtr : MonoBehaviour {
  public  Toggle toggle;
    // Use this for initialization

    private void Start()
    {
        toggle.isOn = ValueSheet.EnbaleMouseCtr;
    }


    public void ValueChange() {
        ValueSheet.EnbaleMouseCtr = toggle.isOn;
    }

    private void OnEnable()
    {
        
    }


}
