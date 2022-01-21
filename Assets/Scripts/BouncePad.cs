using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{

    public float bounceForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.Bounce(bounceForce); // call the Bounce function in the PlayerController script, passing over the bounceForce we have set on the bouncepad
            AudioManager.instance.PlaySFX(0); // play sfx element from audio manager SFX list

        }
    }
}
