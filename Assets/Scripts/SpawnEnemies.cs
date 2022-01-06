using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    public GameObject theEnemy;
    private int xPos;
    private int yPos;
    private int zPos;
    public int spawnCount;
    private int enemyCounter = 0;
    public float spawnInterval, spawnDelayTime;
    public int yPosLow, yPosHigh;
    private float spawnDelayCounter;
    // Start is called before the first frame update
    void Start()
    {
        spawnDelayCounter = spawnDelayTime;
    }

    private void Update()
    {
        if (spawnDelayCounter > 0)
        {
            // Countdown timer
            spawnDelayCounter -= Time.deltaTime;
            // Debug.Log("spawnDelayCounter:" + spawnDelayCounter);
        }

        if (spawnDelayCounter <= 0)
        {
            StartCoroutine(EnemySpawn());
        }
    }

    IEnumerator EnemySpawn()
    {
        while (enemyCounter < spawnCount)
        {
            xPos = Random.Range(-17, 1);
            yPos = Random.Range(yPosLow, yPosHigh);
            zPos = Random.Range(-30, 23);
            Instantiate(theEnemy, new Vector3(xPos, yPos, zPos), Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
            enemyCounter++;
        }
    }
}