using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GD.MinMaxSlider;

public class SpawnerTest1 : MonoBehaviour
{
    [Header("Spawn Characteristics")]
    [Space]
    [Space]
    public bool activateSpawner;
    [Space]
    public GameObject positionRefObj;
    [Space]
    public int spawnCount;
    [Space]
    [Space]
    public GameObject[] preFabsToSpawn;
    private int itemToSpawn;


    //public int xaxis;
    //public int yaxis;
    //public int zaxis;

    private float xAxis, yAxis, zAxis, objectMinSize;
    private float objectMaxSize, xRand, yRand, zRand;
    public bool doRandomRotation;

    //public float minXRotation;
    //public float maxXRotation;
    //public float minYRotation;
    //public float maxYRotation;
    //public float minZRotation;
    //public float maxZRotation;




    //Min Max Sliders for X,Y,Z offsets
    [MinMaxSlider(0, 100f)]
    public Vector2 XCoordinateRange = new Vector2();

    [MinMaxSlider(0, 100f)]
    public Vector2 YCoordinateRange = new Vector2();

    [MinMaxSlider(0, 100f)]
    public Vector2 ZCoordinateRange = new Vector2();


    //Min Max Sliders for Rotation offsets
    [MinMaxSlider(0, 360f)]
    public Vector2 XRotateRange = new Vector2();

    [MinMaxSlider(0, 360f)]
    public Vector2 YRotateRange = new Vector2();

    [MinMaxSlider(0, 360f)]
    public Vector2 ZRotateRange = new Vector2();


    // Min Max slider for size
    [MinMaxSlider(.01f, 5f)]
    public Vector2 sizeRange = new Vector2();



    private void Start()
    {
        if (positionRefObj != null)
        {
            //hide ref object
            positionRefObj.SetActive(false);
        }

        if (!activateSpawner) { return; }
        // Spawn Test 1
        Spawner1Test();
    }

    public void Spawner1Test()
    {

        // Get X,Y,Z min/max data from the min/max GUI sliders

        var minX = XCoordinateRange.x;
        var maxX = XCoordinateRange.y;

        var minY = YCoordinateRange.x;
        var maxY = YCoordinateRange.y;

        var minZ = ZCoordinateRange.x;
        var maxZ = ZCoordinateRange.y;

        // Seperate the x,y,z from the above
        xAxis = transform.localPosition.x;
        yAxis = transform.localPosition.y;
        zAxis = transform.localPosition.z;

        var minXRotate = XRotateRange.x;
        var maxXRotate = XRotateRange.y;

        var minYRotate = YRotateRange.x;
        var maxYRotate = YRotateRange.y;

        var minZRotate = ZRotateRange.x;
        var maxZRotate = ZRotateRange.y;


        // Get Scale Range from the min/max GUI sliders
        objectMinSize = sizeRange.x;
        objectMaxSize = sizeRange.y;

        // For the spawn count total, loop through each, picking a random range from each of the X,Y,Z min/max, picking a random range from the X,Y,Z min/max rotation and for the min/max scaling

        for (int i = 0; i < spawnCount; i++)
        {
            // Calculate random X, Y, Z Positions
            xRand = Random.Range(xAxis - minX, xAxis + maxX);
            yRand = Random.Range(yAxis - minY, yAxis + maxY);
            zRand = Random.Range(zAxis - minZ, zAxis + maxZ);

            var randomPosition = new Vector3(xRand, yRand, zRand);

            // Calculate the Rotation
            float xRotation = Random.Range(minXRotate, maxXRotate);
            float yRotation = Random.Range(minYRotate, maxYRotate);
            float zRotation = Random.Range(minZRotate, maxZRotate);

            var randomRotation = Quaternion.Euler(xRotation, yRotation, zRotation);

            // Instantiate the game object
            GameObject go = Instantiate(preFabsToSpawn[itemToSpawn], randomPosition, randomRotation);

            // Randomly scale instantiated object based on min / max scale
            float scale = Random.Range(objectMinSize, objectMaxSize);
            go.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}



/*

// ORIGINAL CODE - all works fine, but can't doesn't spawn object starting at the parent transform position

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

                GameObject go = Instantiate(preFabsToSpawn[itemToSpawn], GeneratedPosition(), Quaternion.identity);
                //GameObject go = Instantiate(preFabsToSpawn[itemToSpawn], GeneratedPosition(), Random.rotation);

                // Randomly scale instantiated object based on min / max scale
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

        */