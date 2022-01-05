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
    private int enemyCounter;
    public float spawnInterval;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        while (enemyCounter < spawnCount)
        {
            xPos = Random.Range(-17, 1);
            yPos = Random.Range(0, 5);
            zPos = Random.Range(-30, 23);
            Instantiate(theEnemy, new Vector3(xPos, yPos, zPos), Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
            enemyCounter++;
        }
    }
}



/* -17, 13
1.7, -23


z = 13, 23
x = -17, 1.7 */