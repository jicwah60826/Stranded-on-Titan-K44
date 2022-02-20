using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public float fixedUpdateTime = .005f;

    private Object[] textures;

    public float waitAfterDying;
    private float reSpawnTimer;
    [HideInInspector] public bool levelEnding;
    [Header("Way Point Markers")]
    [Tooltip("The parent game object item for ALL waypoint markersa in the game / level")]
    public GameObject wayPointMarkers;
    [Tooltip("Select if Waypoint indicator should be enabled in the game")]
    public bool markersOn = false;

    private void Awake()
    {
        /* This doubles the number of times per second the fixedUpdate will run. You can keep decreasing the multiplier to increase number of checks. */
        Time.fixedDeltaTime = Time.timeScale * fixedUpdateTime; // Normally multiplied by 0.02 to make 50 checks a second.

        textures = Resources.LoadAll("Art", typeof(Texture2D)); // load all textures on game start

        instance = this; // allow this script to be accessed anywhere
    }
    // Start is called before the first frame update
    void Start()
    {
        // Hide / Lock cursor movement in game window
        Cursor.lockState = CursorLockMode.Locked;

        //Set Cursor to not be visible
        Cursor.visible = false;

        WayPointMarkers();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnPause();
        }

        if (Input.GetKeyDown(KeyCode.Y) && PlayerPrefs.GetString("wayPointsAllowed") == "true")
        {
            // toggle markers on/off
            markersOn = !markersOn;
            Debug.Log("wayPointsAllowed are allowed, now invoking function WayPointMarkers");
            WayPointMarkers();
        }

        HandlePlayerPrefs();

    }

    public void HandlePlayerPrefs()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //clear all player prefs;
            PlayerPrefs.DeleteAll();
            Debug.Log("all player prefs cleared");
            // Disable things related to player abilities
            wayPointMarkers.SetActive(false);
        }
    }




    public void WayPointMarkers()
    {
        //if (PlayerPrefs.GetString("wayPointsAllowed") == "true")
        //{
        if (markersOn)
        {
            Debug.Log("markers are enabled in scene");
            wayPointMarkers.SetActive(true);
        }
        else if (!markersOn)
        {
            Debug.Log("markers are disabled in scene");
            wayPointMarkers.SetActive(false);
        }
        //}



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

        // NOTE: this function is only called when we hit the escape key

        // If pause menus is active in the hierarchy - then disable it and return to the game
        if (UIController.instance.pauseScreen.activeInHierarchy)
        {
            // disable the pause menu overlay
            UIController.instance.pauseScreen.SetActive(false);

            // lock the cursor back onto the screen
            Cursor.lockState = CursorLockMode.Locked;

            // un-freeze time
            Time.timeScale = 1f;

            // Hide cursor
            Cursor.visible = false;
        }
        else
        {
            // enable the pause menu overlay
            UIController.instance.pauseScreen.SetActive(true);

            // unlock the cursor so user can navigate the menus
            Cursor.lockState = CursorLockMode.None;

            // Show Cursor
            Cursor.visible = false;

            // freeze time
            Time.timeScale = 0f;
        }
    }
}
