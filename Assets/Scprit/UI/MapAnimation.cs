using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class MapAnimation : MonoBehaviour {
    public Animator MapAnimator;
    public float AnimProportion;
    // Use this for initialization
    void Start () {

    }

    public void initialization() {
        MapAnimator = this.GetComponent<Animator>();

       
       // MapAnimator.speed = Cspeed;
    }

    // Update is called once per frame
    void Update () {
        if (MeshVideo.instance != null)
        {
            AnimProportion = MeshVideo.instance.VideoProportion;
            MapAnimator.Play("MapAnimation01", 0, AnimProportion);
        }
    }
}
