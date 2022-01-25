using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    public string nextLevel;
    public float waitToEndLevel;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            Debug.Log("load next level");
            StartCoroutine(EndLevelCo());
            GameManager.instance.levelEnding = true;
            AudioManager.instance.LevelVictory();
            //clear checkpoint
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", ""); // clear out stored value for cpname
        }
    }

    private IEnumerator EndLevelCo(){
        yield return new WaitForSeconds(waitToEndLevel);
        SceneManager.LoadScene(nextLevel);
    }
}