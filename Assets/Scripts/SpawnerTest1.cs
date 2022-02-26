using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTest1 : MonoBehaviour
{
    //public GameObject itemToSpawn;
    public bool activateSpawner;
    public GameObject[] preFabsToSpawn;
    private int itemToSpawn;
    public int number;
    public int xaxis;
    public int yaxis;
    public int zaxis;
    public bool doRandomRotation;

    public float objectMinSize;
    public float objectMaxSize;

    void Start()
    {
        if (activateSpawner)
        {
            PlaceItem();
        }


    }
    void PlaceItem()
    {
        
        for (int i = 0; i < number; i++)
        {

            itemToSpawn = Random.Range(0, preFabsToSpawn.Length);
            //GameObject go = Instantiate(preFabsToSpawn[itemToSpawn], GeneratedPosition(), Quaternion.identity);
            GameObject go = Instantiate(preFabsToSpawn[itemToSpawn], GeneratedPosition(),Random.rotation);
            float scale = Random.Range(objectMinSize, objectMaxSize);
            go.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
    Vector3 GeneratedPosition()
    {
        int x, y, z;

        x = Random.Range(-xaxis, xaxis);
        y = Random.Range(-yaxis, yaxis);
        z = Random.Range(-zaxis, zaxis);

        return new Vector3(x, y, z);
    }
}