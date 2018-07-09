using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NImage : ImageBase {

  public VideoFrameClickEvent clickEvent;
    // Use this for initialization

    public void Awake()
    {

    }

    public new void Start () {
        initialization();
    }


    // Update is called once per frame
    void Update () {

	}

    public new virtual  void initialization() {
        base.initialization();

        clickEvent = this.GetComponent<VideoFrameClickEvent>();
    }
}
