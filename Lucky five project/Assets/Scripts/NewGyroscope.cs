using UnityEngine;
using Cinemachine;

public class NewGyroscope : MonoBehaviour
{
    private UnityEngine.Gyroscope phoneGyro;
    private Quaternion correctionQuaternion;
    public CinemachineFreeLook virtualCamera;

    void Awake()
    {
        phoneGyro = Input.gyro;
        phoneGyro.enabled = true;
        correctionQuaternion = Quaternion.Euler(90f, 0f, 0f);
    }

    void Update()
    {
        GyroModifyCamera();
    }

    void GyroModifyCamera()
    {
        Quaternion gyroQuaternion = GyroToUnity(Input.gyro.attitude);
        Quaternion calculatedRotation = correctionQuaternion * gyroQuaternion;
        // Aggiorna la rotazione della Cinemachine Virtual Camera
        virtualCamera.transform.rotation = new Quaternion(0, calculatedRotation.y, 0, calculatedRotation.w);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
