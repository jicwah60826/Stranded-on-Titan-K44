using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropController : MonoBehaviour
{
    [Header("Loot Characteristics")]
    [Space]
    [Space]
    public bool activateLootDrop;
    [Space]
    [Space]
    //Min Max Sliders Spawn Count
    [MinMaxSlider(0, 1000)]
    public Vector2 spawnCountRange = new Vector2();

    public GameObject[] lootDropItems;
    private int itemToSpawn, numToSpawn;

    private float xAxis, yAxis, zAxis, objectMinSize;
    private float objectMaxSize, xRand, yRand, zRand;

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
        if (!activateLootDrop) { return; }

        SpawnLoot();
    }

    public void SpawnLoot()
    {

        //generate random spawn amount
        var spawnCountMin = spawnCountRange.x;
        var spawnCountMax = spawnCountRange.y;

        int spawnMin = (int)spawnCountMin;
        int spawnMax = (int)spawnCountMax;

        numToSpawn = Random.Range(spawnMin, spawnMax);

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

        for (int i = 0; i < numToSpawn; i++)
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
            GameObject go = Instantiate(lootDropItems[itemToSpawn], randomPosition, randomRotation);

            // Randomly scale instantiated object based on min / max scale
            float scale = Random.Range(objectMinSize, objectMaxSize);
            go.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
