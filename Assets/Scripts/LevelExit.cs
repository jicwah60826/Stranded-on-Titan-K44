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
            AudioManager.instance.PlaySFX(13); // play level exit from audio manager SFX list
            GameManager.instance.levelEnding = true;
            AudioManager.instance.StopBGM();
        }
    }

    private IEnumerator EndLevelCo(){
        yield return new WaitForSeconds(waitToEndLevel);
        SceneManager.LoadScene(nextLevel);
    }
}