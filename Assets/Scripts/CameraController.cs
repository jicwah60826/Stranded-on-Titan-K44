using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;

    // Update is called once per frame
    void LateUpdate()
    {
        // Late Update ensures that this is done AFTER the main update so that the PlayerController script is not fighting with this script.

        // move the camera to the same location as the specified target object
        transform.position = target.position;

        // rotate the camera the same as the specified target object
        transform.rotation = target.rotation;

    }
}
