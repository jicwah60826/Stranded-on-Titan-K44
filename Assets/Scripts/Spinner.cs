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
        transform.Rotate(xRotation, yRotation, zRotation);
    }
}
