using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;

    public Transform target;

    private float startFOV;
    private float targetFOV;

    public float zoomSpeed; // how fast we zoom in down gun sights

    public Camera theCam;

    private void Awake() {
        instance = this;
    }

    private void Start()
    {
        startFOV = theCam.fieldOfView;
        targetFOV = startFOV;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Late Update ensures that this is done AFTER the main update so that the PlayerController script is not fighting with this script.

        // move the camera to the same location as the specified target object
        transform.position = target.position;

        // rotate the camera the same as the specified target object
        transform.rotation = target.rotation;

        theCam.fieldOfView = Mathf.Lerp(theCam.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime); // smooth move between the 2 values at a certain speed

    }

    public void ZoomIn(float newZoom)
    {
        targetFOV = newZoom;
    }

    public void ZoomOut()
    {
        targetFOV = startFOV;
    }
}