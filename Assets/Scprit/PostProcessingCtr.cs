using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingCtr : MonoBehaviour {
    public static   PostProcessingCtr instance;
    public PostProcessVolume postProcessVolume;
    public void Awake()
    {
        postProcessVolume = this.GetComponent<PostProcessVolume>();

        if (instance == null) {
            instance = this;
        }
    }

    public void Blur() {
        changeBlurAmount(1, 300, 1, LeanTweenType.easeInOutSine);
    }

    public void Focus()
    {
        changeBlurAmount(300, 1, 1, LeanTweenType.easeInOutSine);

    }

    public void changeBlurAmount(float from, float to, float time, LeanTweenType leanTweenType = LeanTweenType.notUsed) {
        DepthOfField depthOfField = postProcessVolume.profile.GetSetting<DepthOfField>();
        LeanTween.value(from, to, time).setOnUpdate(delegate (float value)
        {
            depthOfField.focalLength.value = value;
        }).setEase(leanTweenType);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
