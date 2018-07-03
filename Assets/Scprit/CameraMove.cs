using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMove : MonoBehaviour {
    public float XRange=8,YRange=7;

    Camera mCamera;

    Transform CamTrans;

    public float buffer;

    void initialization() {
        Screen.SetResolution(1920, 1200, true);

        mCamera = this.GetComponent<Camera>();

        CamTrans = this.GetComponent<Transform>();
    }

    private void Awake()
    {

        initialization();

    }
	
	// Update is called once per frame
	void Update () {

        MoveBehavior();

    }

    void MoveBehavior() {
        float XAxis=   Input.GetAxis("Horizontal")*buffer;

        float YAxis = Input.GetAxis("Vertical")*buffer;

        float Yaw = Input.GetAxis("Rotate");

        float posx = Mathf.Clamp(CamTrans.position.x + XAxis, -XRange, XRange);

        float posy = Mathf.Clamp(CamTrans.position.y + YAxis, -YRange, YRange);


        CamTrans.position = new Vector3(posx,posy, CamTrans.position.z);

        CamTrans.localRotation = Quaternion.Euler(CamTrans.localRotation.eulerAngles.x, CamTrans.localRotation.eulerAngles.y, CamTrans.localRotation.eulerAngles.z - Yaw);


    }


}
