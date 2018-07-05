using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using RenderHeads.Media.AVProVideo;

[RequireComponent(typeof(MediaPlayer))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(NImage))]
public class VideoBase : MonoBehaviour {
    protected NImage nImage;

    protected MediaPlayer mediaPlayer;

    public float currentTime;

    public float durationTime;

    public float VideoProportion {
        get
        {
            return getCurrentTime() / getDurationTime();
        }
    }

    public string path;   


   public virtual void initialization() {

        nImage = this.GetComponent<NImage>();

       mediaPlayer = this.GetComponent<MediaPlayer>();

        LoadVideo(path);
    }

    public void SetVideoPath(string str) {
        path = "Video/" + str;
    }

    public void playVideo() {
        mediaPlayer.Play();
    }

    public void StopVideo() {
        mediaPlayer.Stop();
    }

    public void PauseVideo() {
        mediaPlayer.Pause();
    }

    public void SetMovieTime(float value) {
        float temp = nImage.Mapping(value, 0f, 1f, 0, getDurationTime());
        mediaPlayer.Control.Seek(temp*1000);
    }

    public void ShowVideo(float time) {
        nImage.ShowAll(time);
    }

    public void HideVideo(float time) {
        nImage.HideAll(time);
    }

    public void PopUp() {
        nImage.SetScale(Vector3.one, 1f);
    }

    public void ShinkDown() {
        nImage.SetScale(Vector3.zero, 1f);
    }

    float getCurrentTime() {
        return mediaPlayer.Control.GetCurrentTimeMs() / 1000f;
    }

    float getDurationTime() {
        return mediaPlayer.Info.GetDurationMs() / 1000f;
    }


    public void setSlider(float value) {

        mediaPlayer.Pause();

        float temp =    nImage.Mapping(value, 0f, 1f, 0f, getDurationTime()) * 1000;

        mediaPlayer.Control.Seek(temp);

        mediaPlayer.Play();
    }



    public void LoadVideo(string _path, bool isAutoPlay = true) {
        path = _path;
        mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, _path, isAutoPlay);
    }

}
