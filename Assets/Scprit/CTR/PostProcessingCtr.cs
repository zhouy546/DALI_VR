using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingCtr : MonoBehaviour {
    public static   PostProcessingCtr instance;
    public PostProcessVolume postProcessVolume;
    DepthOfField depthOfField; 

    public void Awake()
    {
        postProcessVolume = this.GetComponent<PostProcessVolume>();
        depthOfField = postProcessVolume.profile.GetSetting<DepthOfField>();
        if (instance == null) {
            instance = this;
        }
    }

    public void Blur() {
        enableBlur();
        changeBlurAmount(1, 300, 1, LeanTweenType.easeInOutSine);
    }

    public void Focus()
    {
        disableBlure();
        changeBlurAmount(300, 1, 1, LeanTweenType.easeInOutSine);

    }

    public void changeBlurAmount(float from, float to, float time, LeanTweenType leanTweenType = LeanTweenType.notUsed) {
        LeanTween.value(from, to, time).setOnUpdate(delegate (float value)
        {
            depthOfField.focalLength.value = value;
        }).setEase(leanTweenType);
    }

    private  void enableBlur() {
        depthOfField.active = true;
    }

    private void disableBlure() {
        depthOfField.active = false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
