﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleMouseCtr : MonoBehaviour {
  public  Toggle toggle;
    // Use this for initialization

    private void Start()
    {
        CameraMovement_hemisphere.EnbaleMouseCtr= toggle.isOn = false;
    }


    public void ValueChange() {
        CameraMovement_hemisphere.EnbaleMouseCtr = toggle.isOn;
    }

    private void OnEnable()
    {
        
    }


}
