using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessingCtr : MonoBehaviour {
    public static PostProcessingCtr instance;
    public PostProcessingBehaviour postProcessVolume;
    public DepthOfFieldModel depthOfField;

    public void Awake()
    {
        postProcessVolume = this.GetComponent<PostProcessingBehaviour>();
        depthOfField = postProcessVolume.profile.depthOfField;

        Focus();
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Blur()
    {
        enableBlur();
        changeBlurAmount(10, 0, 1, LeanTweenType.easeInOutSine);
    }

    public void Focus()
    {
        disableBlure();
        changeBlurAmount(0, 10, 1, LeanTweenType.easeInOutSine);

    }

    public void changeBlurAmount(float from, float to, float time, LeanTweenType leanTweenType = LeanTweenType.notUsed)
    {
        LeanTween.value(from, to, time).setOnUpdate(delegate (float value)
        {
            DepthOfFieldModel.Settings Newsettings = depthOfField.settings;
            Newsettings.focusDistance = value;

            depthOfField.settings = Newsettings;

        }).setEase(leanTweenType);
    }

    private void enableBlur()
    {
        depthOfField.enabled = true;
    }

    private void disableBlure()
    {
        depthOfField.enabled = false;
    }


}
