using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    private UnityEngine.Gyroscope phoneGyro;
    private Quaternion correctionQuaternion;

    public Vector3 cameraDirection;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        phoneGyro = Input.gyro;
        phoneGyro.enabled = true;
        correctionQuaternion = Quaternion.Euler(90f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
            GyroModifyCamera();
    }

    // The Gyroscope is right-handed.  Unity is left handed.
    // Make the necessary change to the camera.
    void GyroModifyCamera()
    {
        Quaternion gyroQuaternion = GyroToUnity(Input.gyro.attitude);
        // rotate coordinate system 90 degrees. Correction Quaternion has to come first
        Quaternion calculatedRotation = correctionQuaternion * gyroQuaternion;
        transform.rotation = new Quaternion(0, calculatedRotation.y, 0, calculatedRotation.w);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    void UpdateCameraDirection()
    {
        // Aggiorna la direzione della camera
        cameraDirection = transform.forward;
    }
}
