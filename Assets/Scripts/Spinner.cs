using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spinner : MonoBehaviour
{
    [SerializeField] float xRotation = 1f;
    [SerializeField] float yRotation = 1f;
    [SerializeField] float zRotation = 1f;
    // Update is called once per frame
    void Update()
    {
        if (!UIController.instance.pauseScreen.activeInHierarchy)
        {
            HandleRotation();
        }
    }

    public void HandleRotation()
    {
        transform.Rotate(xRotation, yRotation, zRotation);
    }
}
