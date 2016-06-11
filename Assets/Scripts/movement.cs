using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
	public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float smthD = 0.1f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private float x, y;
    private float xRotV, yRotV;

    void Update () {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        x = Mathf.SmoothDamp(x, pitch, ref xRotV, smthD);
        y = Mathf.SmoothDamp(y, yaw, ref xRotV, smthD);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

    }
}
