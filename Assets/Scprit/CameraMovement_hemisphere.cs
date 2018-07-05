using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement_hemisphere : MonoBehaviour {
    //public static bool EnbaleMouseCtr = false;

   // public static float EaseingValue = 0.013f;

   // public static Vector3 DefaultCameraRot;

    //public static Vector3 CamRotation;

    Camera mCamera;

    Transform CamTrans;

    public Transform CamV;

    public Transform CamZ;


    float ZRot;
    public IEnumerator initialization()
    {
        Screen.SetResolution(1920, 1200, true);

        yield return mCamera = this.GetComponent<Camera>();

        yield return CamTrans = this.GetComponent<Transform>();

        SetupCameraAngle();
    }

    void SetupCameraAngle() {
        CamV.localRotation = Quaternion.Euler(new Vector3(ValueSheet.DefaultCameraRot.x, 0, 0));
        CamZ.localRotation = Quaternion.Euler(new Vector3(0, 0, ValueSheet.DefaultCameraRot.z));
        CamTrans.localRotation = Quaternion.Euler(new Vector3(0, ValueSheet.DefaultCameraRot.y, 0));
    }

    private void Awake()
    {

    }

    // Use this for initialization
    void Start() {

    }
    private bool onSwitch = true;
    // Update is called once per frame
    void Update() {

        CameraRot();

    }

    void CameraRot() {
        if (mCamera != null && CamTrans != null && ValueSheet.EnbaleMouseCtr)
        {
            if (onSwitch)
            {
                onSwitch = false;
            }

            MouseMovement();

        }
        else if (mCamera != null && CamTrans != null && !ValueSheet.EnbaleMouseCtr)
        {
            if (!onSwitch)
            {
                onSwitch = true;
                SetupCameraAngle();
            }

            UdpMovement(ValueSheet.CamRotation);
        }
    }

    float x, y, z;
    void UdpMovement(Vector3 cameraRotation)
    {
        x = x + (cameraRotation.x - x) * ValueSheet.EaseingValue;
        y = y + (cameraRotation.y - y) * ValueSheet.EaseingValue;
        z = z + (cameraRotation.z - z) * ValueSheet.EaseingValue;

        CamTrans.localRotation = Quaternion.Euler(new Vector3(x,y,z));
    }


    void MouseMovement() {

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
