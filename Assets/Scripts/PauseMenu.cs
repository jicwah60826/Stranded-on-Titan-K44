using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Resume()
    {
        GameManager.instance.PauseUnPause();
    }

    public void MainMenu()
    {
        Debug.Log("Loading Main Menu");
        UIController.instance.pauseScreenOverlay.SetActive(false);
        UIController.instance.blackScreen.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        //Debug.Log("Quitting Application");
        Application.Quit();
        //Debug.Log("Quitting Game");
    }

    public void OptionsMenu()
    {
        //Debug.Log("Loading Options Menu");
    }
}
