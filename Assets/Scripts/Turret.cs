using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public GameObject bullet;
    [Header("Damage Amount & Range")]
    [Tooltip("Minimum random damage intensity")]
    public float rangeToTargetPlayer;
    public float fireRate;
    public float rotateSpeed;
    public float rotateOffset;
    public float lockOnSpeed; // speed at which the turret turns towards the player once in range
    private float shotCounter;
    public Transform gun, firePoint;
    public GameObject rayCastFirePoint;
    private bool turretCanFire;
    private bool lineOfSight;
    private int targetLayer;
    private bool wasShot;


    private float waitToInvoke = 2f; //seconds before the first invoke;
    public float burstInterval; //seconds between every invoke;
    //private bool lineOfSight;

    private void Start()
    {
        shotCounter = fireRate; //initialize shot countdown timer
        // Begin the turrent can fire toggle
        InvokeRepeating("turretCanFireToggle", waitToInvoke, burstInterval);
    }

    private void Update()
    {
        handleShooting();
        LineOfSight();
    }

    private void LineOfSight()
    {
        targetLayer = LayerMask.NameToLayer("Player");

        if (rayCastFirePoint != null)
        {
            var ray = new Ray(rayCastFirePoint.transform.position, rayCastFirePoint.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                Debug.Log("zz1");

                if (hit.transform.gameObject.layer == targetLayer)
                {
                    lineOfSight = true;
                    Debug.Log("zz2");
                }
                else
                {
                    lineOfSight = false;
                    Debug.Log("zz3");
                }
                Debug.Log("Player in lineOfSight = " + lineOfSight);
            }

        }
    }


    private void handleShooting()
    {

        if (PlayerController.instance.gameObject.activeInHierarchy && !GameManager.instance.levelEnding)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToTargetPlayer || wasShot)
            {
                /* SMOOTHLY rotate the turret to face the player once player is within range.
                The Code below takes into account the CURRENT rotation angle of the turret so that
                it rotates from that point to the direction of the player no matter where it is in the rotation cycle
                */

                firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 1.5f, 0f));

                Vector3 newDirection = Vector3.RotateTowards(gun.forward, PlayerController.instance.transform.position - transform.position, rotateSpeed * lockOnSpeed * Time.deltaTime, 0f);
                gun.rotation = Quaternion.LookRotation(newDirection);

                // begin shot counter countdown to see if turret should fire another shot
                shotCounter -= Time.deltaTime;


                // fire a shot evert fireRate cycle. EG: .3 fireRate means we fire a shot every 30th of a second.

                if (lineOfSight)
                {
                    if (shotCounter <= 0 && turretCanFire)
                    {
                        //Fire a bullet
                        Instantiate(bullet, firePoint.position, firePoint.rotation);
                        shotCounter = fireRate; //reset shot counter
                    }
                }

            }
            else if (lineOfSight == false)
            {
                //Debug.Log("Player not within range of turret");
                wasShot = false;
                shotCounter = fireRate; //reset shot counter
                gun.rotation = Quaternion.Lerp(gun.rotation, Quaternion.Euler(0f, gun.rotation.eulerAngles.y + rotateOffset, 0f), rotateSpeed * Time.deltaTime); // rotate the turret around based on rotateSpeed
            }
        }
    }

    private void turretCanFireToggle()
    {
        turretCanFire = !turretCanFire;
        //Debug.Log("turretCanFire = " + turretCanFire);
    }

    public void TurretShot()
    {

        //Enemy shot - make enemy chase towards and / or start firing player
        wasShot = true;
        //chasing = true;
        Debug.Log("zz1 - TurretShot: wasShot = " + wasShot);
    }

}