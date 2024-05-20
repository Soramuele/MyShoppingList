using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GPS gps;
    private double x;
    private double y;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        gps = GetComponent<GPS>();
    }

    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // I wanna kill myself
        x = (gps.latitude - (int)gps.latitude) * 10;
        y = (gps.longitude - (int)gps.longitude) * 10;

        transform.position = new Vector3((float)x, 1, (float)y);
        transform.rotation = new Quaternion(0, Input.gyro.attitude.y, 0, 0);
    }
}
