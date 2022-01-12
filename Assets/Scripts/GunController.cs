using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public GameObject bullet;

    public bool canAutoFire;
    public float fireRate;

    public int currentAmmo;

    [HideInInspector]
    public float fireCounter; //making this accesible to other scripts, but not visible in the inspector

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fireCounter > 0)
        {
            fireCounter -= Time.deltaTime; //begin the fireCounter countdown after each shot
        }
    }
}
