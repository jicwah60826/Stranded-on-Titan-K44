/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    public GameObject theEnemy;
    private int xPos;
    private int yPos;
    private int zPos;
    public int enemyCount;
    public int yPosLow, yPosHigh;

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while (enemyCount < 1)
        {
            xPos = Random.Range(-17, 1);
            yPos = Random.Range(1, 10);
            zPos = Random.Range(-30, 23);
            Instantiate(theEnemy, new Vector3(xPos, yPos, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }
} */

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
