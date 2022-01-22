using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBtnAudio : MonoBehaviour
{

    public AudioSource menuSFX;
    public AudioClip btnHoverSFX;
    public AudioClip btnClickSFX;

    public void hoverSFX()
    {
        menuSFX.PlayOneShot(btnHoverSFX);
    }

    public void clickSFX()
    {
        menuSFX.PlayOneShot(btnClickSFX);
    }
}
