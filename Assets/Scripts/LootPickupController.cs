using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickupController : MonoBehaviour
{

    public enum LootType { Metal, Wires, Crystals }

    public LootType lootType;

    private int currentLoot;

    public float deSpawnTime;

    private void Start()
    {
        Debug.Log("lootType = " + lootType);

        // get current loot that player has

        StartCoroutine(DeSpawnController());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // add loot to current loot count

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
