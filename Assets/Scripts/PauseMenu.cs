using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Resume()
    {
        GameManager.instance.PauseUnPause();
    }

    public void MainMenu()
    {

    }

    public void QuitGame()
    {
        //Debug.Log("Quitting Application");
        Application.Quit();
    }

    public void OptionsMenu()
    {

    }
}
