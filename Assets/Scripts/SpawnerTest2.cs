using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTest2 : MonoBehaviour
{

    Vector3 originPoint;
    public int spawnRadius;
    public int spawnCount;
    public GameObject itemToSpawn;

    private void Start()
    {
        GameObject spawner = this.gameObject;

        originPoint = spawner.gameObject.transform.position;

        CreateAgent();
    }

    public void CreateAgent()
    {

        for (int i = 0; i < spawnCount; i++)
        {
            float directionFacing = Random.Range(0f, 360f);
            // need to pick a random position around originPoint but inside spawnRadius
            // must not be too close to another agent inside spawnRadius
            Vector3 point = (Random.insideUnitSphere * spawnRadius) + originPoint;
            Instantiate(itemToSpawn, point, Quaternion.Euler(new Vector3(0f, directionFacing, 0f)));
        }
    }

}
