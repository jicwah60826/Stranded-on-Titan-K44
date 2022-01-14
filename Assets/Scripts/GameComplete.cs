using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameComplete : MonoBehaviour
{

    public float timeBetweenShowing;

    public GameObject victoryText, mainMenuButton, quitButton;

    public Image blackScreen;
    public float blackScreenFade;

    private void Start()
    {
        StartCoroutine(ShowObjectsCo());
    }

    private void Update() {
        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, blackScreenFade * Time.deltaTime));
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0); // load the main menu scene
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Application");
        Application.Quit();
    }

    public IEnumerator ShowObjectsCo()
    {

        //hide all
        victoryText.SetActive(false);
        mainMenuButton.SetActive(false);
        quitButton.SetActive(false);

        // Initial Wait
        yield return new WaitForSeconds(timeBetweenShowing);

        //show textbox
        victoryText.SetActive(true);

        // Wait
        yield return new WaitForSeconds(timeBetweenShowing);

        //show mainMenuButton
        mainMenuButton.SetActive(true);

        // Wait
        yield return new WaitForSeconds(timeBetweenShowing);

        //show mainMenuButton
        quitButton.SetActive(true);


    }
}
