using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastTest : MonoBehaviour
{
    public GameObject firePoint;
    private int targetLayer;
    private bool lineOfSight;

    void Update()
    {
        targetLayer = LayerMask.NameToLayer("Player");

        if (firePoint != null)
        {
            var ray = new Ray(firePoint.transform.position, firePoint.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {

                if (hit.transform.gameObject.layer == targetLayer)
                {
                    lineOfSight = true;
                }
                else
                {
                    lineOfSight = false;
                }
                Debug.Log("Player in lineOfSight = " + lineOfSight);
            }

        }

    }
}
