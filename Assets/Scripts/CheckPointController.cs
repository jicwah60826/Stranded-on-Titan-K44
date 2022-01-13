using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointController : MonoBehaviour
{

    public string cpName;

    private void Start()
    {
        HandleCheckPointSpawn();
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
                Debug.Log("Player starting at" + cpName);
            }

        }
    }

    private static void ClearCheckPointSpawn()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", ""); // clear out stored value for cpname
            Debug.Log("cpName cleared from playerprefs");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", cpName);
            Debug.Log("Player hit" + cpName);
        }
    }
}