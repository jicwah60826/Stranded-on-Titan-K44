using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickupController : MonoBehaviour
{

    private enum LootType { Metal, Wires, Crystals }

    private LootType lootType;

    int lootCount;

    public float deSpawnTime;

    private void Start()
    {
        StartCoroutine(DeSpawnController());

        lootType = LootType.Metal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            switch (lootType)
            {
                case LootType.Metal:
                    // do stuff if Metal
                    int lootCount = PlayerPrefs.GetInt("MetalCount", 0);
                    // add to loot
                    lootCount++;
                    // update player prefs
                    PlayerPrefs.SetInt("MetalCount", lootCount);
                    break;
                case LootType.Crystals:
                    // do stuff if Crystals
                    break;
                case LootType.Wires:
                    // do stuff if Wires
                    break;
            }

            // play pickup sound
            AudioManager.instance.PlaySFX(5); // play sfx element from audio manager SFX list

            // destrot this game object
            Destroy(gameObject);
        }
    }

    private IEnumerator DeSpawnController()
    {

        yield return new WaitForSeconds(deSpawnTime);
        Destroy(gameObject);


    }
}
