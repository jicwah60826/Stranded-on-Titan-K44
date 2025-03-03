using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointController : MonoBehaviour
{

    public string cpName;
    private int wayPointAbility;

    private void Start()
    {
        HandleCheckPointSpawn();

        if (PlayerPrefs.HasKey("wayPointsAbility"))
        {
         wayPointAbility = PlayerPrefs.GetInt("wayPointsAbility", 0);
         //Debug.Log("wayPointAbility = " + wayPointAbility);
        }
    }

    private void Update()
    {
        ClearCheckPointSpawn();
    }

    private void HandleCheckPointSpawn()
    {
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_cp"))
        {
            if (PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "_cp") == cpName)
            {
                // do stuff
                PlayerController.instance.transform.position = transform.position;
                //Debug.Log("Player starting at" + cpName);
            }

        }
    }

    private static void ClearCheckPointSpawn()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", ""); // clear out stored value for cpname
                                                                                   //Debug.Log("cpName cleared from playerprefs");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", cpName);
            AudioManager.instance.PlaySFX(1); // play sfx element from audio manager SFX list
                                              //Debug.Log("Player hit" + cpName);
        }
    }
}