using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    private float scopedSensitivity = 1f;

    public Transform playerBody;

    private float xRotation = 0f;

    private Vector3 currentRotation;
    private Vector3 rot;
    private float rotationSpeed = 6f;
    private float returnSpeed = 25f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetSensitivity(float val)
    {
        scopedSensitivity = val;
    }

    public void SetSpeeds(float returnSpeed, float rotationSpeed)
    {
        this.returnSpeed = returnSpeed;
        this.rotationSpeed = rotationSpeed;
    }

    public void AddRecoil(float x, float y, float z)
    {
        currentRotation += new Vector3(x, y, z);
    }

    public void Rotate(float x, float y)
    {
        x *= mouseSensitivity * scopedSensitivity * Time.deltaTime;
        y *= mouseSensitivity * scopedSensitivity * Time.deltaTime;
        
        xRotation -= y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * x);

        currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        rot = Vector3.Slerp(rot, currentRotation, rotationSpeed * Time.deltaTime);
    }
   
}
