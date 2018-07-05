using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CamEaseCtr : MonoBehaviour {
    public InputField InputFieldPort;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnEnable()
    {
        UpdateInputText(GetValue());
    }

    public float GetValue()
    {
        float value = ValueSheet.EaseingValue;

        return value;
    }

    public void setValue()
    {
        ValueSheet.EaseingValue = float.Parse(InputFieldPort.text);

    }

    public void UpdateInputText(float value)
    {
        InputFieldPort.text = value.ToString();

    }
}
