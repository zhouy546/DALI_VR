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
        //Screen.SetResolution(1920, 1200, true);

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
    void /*Fixed*/Update() {

        CameraRot();

    }

    void CameraRot() {
        if (mCamera != null && CamTransZ != null )
        {
            if (!onSwitch)
            {
                onSwitch = true;
                SetupCameraAngle();
            }

            JoyStickMovement(ref ValueSheet.CamRotation);
            //Debug.Log("RUNNING");
        }
    }


    float tempzAxis, tempxAxis;
    float currentZAxis, currentXAxis;
    void JoyStickMovement(ref Vector3 cameraRotation)
    {

        float zAxis = -Input.GetAxis("Mouse X");
        float xAxis = Input.GetAxis("Mouse Y");

        #region value filter 
        //--z--filter
        if (tempzAxis == 0 && zAxis == 0)
        {
            currentZAxis = 0;
        }
        else if (zAxis == 0 && tempzAxis != 0) {
            currentZAxis = Mathf.Clamp( tempzAxis,-1.5f,1.5f);
        } else if (zAxis != 0 && tempzAxis != 0) {
            currentZAxis = Mathf.Clamp(zAxis, -1.5f, 1.5f);
        }
        //----X---filter
        if (tempxAxis == 0 && xAxis == 0)
        {
            currentXAxis = 0;
        }
        else if (xAxis == 0 && tempxAxis != 0)
        {
            currentXAxis = Mathf.Clamp(tempxAxis, -1.5f, 1.5f);
        }
        else if (xAxis != 0 && tempxAxis != 0)
        {
            currentXAxis = Mathf.Clamp(xAxis, -1.5f, 1.5f);
        }
        #endregion

        float cameraZ = cameraRotation.z;

        float cameraX = cameraRotation.x;

        float cameraY = cameraRotation.y;

        //Debug.Log(cameraZ.ToString());

       float  z = UtilityFunction.Mapping(currentZAxis, -1.5f, 1.5f,ValueSheet.Cam_Z_RotMinium, ValueSheet.Cam_Z_RotMaxium);

       float  x = UtilityFunction.Mapping(currentXAxis, -1.5f, 1.5f, ValueSheet.Cam_X_RotMinium, ValueSheet.Cam_X_RotMaxium);

       float  y = UtilityFunction.Mapping(z, ValueSheet.Cam_Z_RotMinium, ValueSheet.Cam_Z_RotMaxium, ValueSheet.Cam_Y_RotMaxium, ValueSheet.Cam_Y_RotMinium);



        cameraZ = cameraZ + (z - cameraZ) * ValueSheet.EaseingValue;

        cameraX = cameraX + (x - cameraX) * ValueSheet.EaseingValue;

        cameraY = cameraY + (y - cameraY) * ValueSheet.EaseingValue;

        cameraRotation = new Vector3(cameraX, cameraY, cameraZ);

        CamTransZ.localRotation = Quaternion.Euler(new Vector3(0, 0, cameraRotation.z));

        CamX.localRotation = Quaternion.Euler(new Vector3(cameraRotation.x, 0, 0));

        CamY.localRotation = Quaternion.Euler(new Vector3(0, cameraRotation.y, 0));

        tempzAxis = zAxis;

        tempxAxis = xAxis;



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
