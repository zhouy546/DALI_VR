using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshVideo : VideoBase {
    public static MeshVideo instance;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    //float tempSpeedTurbo1Axis, tempSpeedTurbo2Axis, tempSpeedTurbo3Axis;
    void Update () {
        if (mediaPlayer != null) {

            if (mediaPlayer.m_Control.IsFinished()) {

                mediaPlayer.Play();

                if (!CanvasCtr.instance.isMenuOn) {

                    CanvasCtr.instance.showMenu();

                }
                                      }
        }
        //float turbo1 = UtilityFunction.Mapping(Input.GetAxis("SpeedTurbo1"), -1f, 1f, .5f, 0f);
        //float turbo2 = UtilityFunction.Mapping(Input.GetAxis("SpeedTurbo2"), -1f, 1f, .5f, 0f);
        //float turbo3 = UtilityFunction.Mapping(Input.GetAxis("SpeedTurbo3"), -1f, 1f, .5f, 0f);

        //// Debug.Log(turbo2);
        //if (mediaPlayer != null)
        //{

        //    if (tempSpeedTurbo1Axis != turbo1 || tempSpeedTurbo2Axis != turbo2 || tempSpeedTurbo3Axis != turbo3)
        //    {

        //        float temp = ValueSheet.playBackRate + turbo1 + turbo2 + turbo3;

        //        mediaPlayer.m_Control.SetPlaybackRate(temp);
        //        mediaPlayer.m_PlaybackRate = temp;
        //        Debug.Log(mediaPlayer.m_PlaybackRate);
        //    }
        //}

        //tempSpeedTurbo1Axis = turbo1;

        //tempSpeedTurbo2Axis = turbo2;

        //tempSpeedTurbo3Axis = turbo3;


    }

    public override void initialization() {
        
        base.initialization();

        if (instance == null) {
            instance = this;
        }

        mediaPlayer.m_PlaybackRate = ValueSheet.playBackRate;
    }
}
