using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform camera;
    public float sensitivityX;
    public float sensitivityY;

    public float minAngle;
    public float maxAngle;

    public float minHeight;
    public float maxHeight;

    private float rotX;
    private float rotY;

    // Update is called once per frame
    void Update()
    {
        rotY += Input.GetAxisRaw("Mouse X") * sensitivityX * Time.deltaTime;
        rotX -= Input.GetAxisRaw("Mouse Y") * sensitivityY * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, minAngle, maxAngle);
        

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotY, transform.eulerAngles.z);
        camera.localEulerAngles = new Vector3(rotX, 0, 0);

    }
}
