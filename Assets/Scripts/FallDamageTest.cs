using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamageTest : MonoBehaviour
{

    public CharacterController charCon;

    private float verticalSpeed, fallStartPos, fallEndPos, fallHeight;

    private bool isFalling;

    public float fallDistanceThreshold;

    private int fallDamageAmount;

    void Update()
    {

/*         verticalSpeed = charCon.velocity.y;
        //Debug.Log("verticalSpeed: " + verticalSpeed);

        if (verticalSpeed <= -1f)
        {
            // Debug.Log("Player is falling");
            isFalling = true;
            // When player begins to fall with a velocity -0.5 or higher, Log Player position (fallStartHeight)
            fallStartPos = transform.position.y;
            Debug.Log("fallStartPos: " + fallStartPos);
        }
        else
        {
            isFalling = false;
            //Log Player position when falling ends
            fallEndPos = transform.position.y;
            Debug.Log("fallEndPos: " + fallEndPos);
        }

        // calculate how far we just fell
        fallHeight = fallStartPos - fallEndPos;
        //Debug.Log("fallHeight: " + fallHeight);

        if (fallHeight <= .5f)
        {
            Debug.Log("OUCH!!!");
            // if the player fell more than the fall threshold, then player receives damage
            fallDamageAmount = 5;
            PlayerHealthController.instance.DamagePlayer(fallDamageAmount);
            fallHeight = 0;
        }

        Debug.Log("isFalling = " + isFalling); */
    }
}
