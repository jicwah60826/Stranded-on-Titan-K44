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
        Debug.Log("Loading Main Menu");
    }

    public void QuitGame()
    {
        //Debug.Log("Quitting Application");
        Application.Quit();
        Debug.Log("Quitting Game");
    }

    public void OptionsMenu()
    {
        Debug.Log("Loading Options Menu");
    }
}
