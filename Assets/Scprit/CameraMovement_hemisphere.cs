using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement_hemisphere : MonoBehaviour {
    //public static bool EnbaleMouseCtr = false;

   // public static float EaseingValue = 0.013f;

   // public static Vector3 DefaultCameraRot;

    //public static Vector3 CamRotation;

    Camera mCamera;

    Transform CamTransZ;

    public Transform CamY;

    public Transform CamX;


    float ZRot;
    public IEnumerator initialization()
    {
        Screen.SetResolution(1920, 1200, true);

        yield return mCamera = this.GetComponent<Camera>();

        yield return CamTransZ = this.GetComponent<Transform>();

        SetupCameraAngle();
    }

    void SetupCameraAngle() {
        CamY.localRotation = Quaternion.Euler(new Vector3(ValueSheet.DefaultCameraRot.x, 0, 0));
        CamX.localRotation = Quaternion.Euler(new Vector3(0, 0, ValueSheet.DefaultCameraRot.z));
        CamTransZ.localRotation = Quaternion.Euler(new Vector3(0, ValueSheet.DefaultCameraRot.y, 0));
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
        if (mCamera != null && CamTransZ != null && ValueSheet.EnbaleMouseCtr)
        {
            if (onSwitch)
            {
                onSwitch = false;
            }

            MouseMovement();

        }
        else if (mCamera != null && CamTransZ != null && !ValueSheet.EnbaleMouseCtr)
        {
            if (!onSwitch)
            {
                onSwitch = true;
                SetupCameraAngle();
            }

            UdpMovement(ValueSheet.CamRotation);
            Debug.Log("RUNNING");
        }
    }

    float x, y, z;
    void UdpMovement(Vector3 cameraRotation)
    {
        x = x + (cameraRotation.x - x) * ValueSheet.EaseingValue;
        y = y + (cameraRotation.y - y) * ValueSheet.EaseingValue;
        z = z + (cameraRotation.z - z) * ValueSheet.EaseingValue;

        CamTransZ.localRotation = Quaternion.Euler(new Vector3(0,0,z));
        CamY.localRotation = Quaternion.Euler(new Vector3(0, y, 0));
        CamX.localRotation = Quaternion.Euler(new Vector3(x, 0, 0));
        //CamTransZ.localRotation = new Quaternion(x, y, z, 0.5f);
    }


    void MouseMovement() {

        float XAxis = Input.GetAxis("Mouse X");

        float YAxis = Input.GetAxis("Mouse Y");

        float Yaw = Input.GetAxis("Rotate");

        float YRot = CamY.localRotation.eulerAngles.y + XAxis;

        float XRot = CamX.localRotation.eulerAngles.x - YAxis;

        float ZRot = CamTransZ.localRotation.eulerAngles.z - Yaw;

        if (checkDeg(YRot, 0.5f))
        {

            CamY.localRotation = Quaternion.Euler(0, YRot, 0);

        }

        if (checkDeg(XRot, 0.866f))
        {

            CamX.localRotation = Quaternion.Euler(XRot, 0, 0);

        }

        //ZRot =ZRot+(XAxis - ZRot)*0.033f;

        CamTransZ.localRotation = Quaternion.Euler(0,0, ZRot);

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
