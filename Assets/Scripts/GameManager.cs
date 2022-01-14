using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private Object[] textures;

    public float waitAfterDying;
    private float reSpawnTimer;


    private void Awake()
    {
        textures = Resources.LoadAll("Art", typeof(Texture2D)); // load all textures on game start

        instance = this; // allow this script to be accessed anywhere
    }
    // Start is called before the first frame update
    void Start()
    {
        // Hide / Lock cursor movement in game window
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnPause();
        }
    }

    public void PlayerDied()
    {
        StartCoroutine(PlayerDiedCo());
    }

    public IEnumerator PlayerDiedCo()
    {
        yield return new WaitForSeconds(waitAfterDying);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // get the name of the currently active scene and re-load / start it over.
    }

    public void PauseUnPause()
    {
        if (UIController.instance.pauseScreen.activeInHierarchy)
        {
            UIController.instance.pauseScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
        else
        {
            UIController.instance.pauseScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
    }
}
