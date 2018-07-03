﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement_hemisphere : MonoBehaviour {
    Camera mCamera;

    Transform CamTrans;

    public Transform CamV;

   public Transform CamZ;

    float ZRot;
   public IEnumerator initialization()
    {
        Screen.SetResolution(1920, 1200, true);

        yield return    mCamera = this.GetComponent<Camera>();

        yield return CamTrans = this.GetComponent<Transform>();
    }

    private void Awake()
    {

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (mCamera != null && CamTrans != null) {

            float XAxis = Input.GetAxis("Mouse X");

            float YAxis = Input.GetAxis("Mouse Y");

            float Yaw = Input.GetAxis("Rotate");

            float YRot = CamTrans.localRotation.eulerAngles.y + XAxis;

            float XRot = CamV.rotation.eulerAngles.x - YAxis;



            if (checkDeg(YRot, 0.5f))
            {

                CamTrans.localRotation = Quaternion.Euler(CamTrans.localRotation.eulerAngles.x, YRot, CamTrans.localRotation.eulerAngles.z);

            }

            if (checkDeg(XRot, 0.866f))
            {

                CamV.localRotation = Quaternion.Euler(XRot, CamV.localRotation.eulerAngles.y, CamV.localRotation.eulerAngles.z);

            }

            //ZRot =ZRot+(XAxis - ZRot)*0.033f;

            //    CamZ.localRotation = Quaternion.Euler(CamZ.localRotation.eulerAngles.x, CamZ.localRotation.eulerAngles.y, -ZRot*10);

        }


    }

    bool checkDeg(float deg,float value) {
        if (Mathf.Abs(Mathf.Sin(deg * Mathf.Deg2Rad)) < value)
        {
            return true;
        }
        else {
            return false;
        }
    }
}
