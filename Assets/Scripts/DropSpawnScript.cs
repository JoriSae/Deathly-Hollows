using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawnScript : MonoBehaviour {
    //variables
    //parent
    public Transform Parent;

    public static DropSpawnScript instance;

        //tier of items affects drop chance
    public GameObject[] Tier1Items;
    public GameObject[] Tier2Items;
    public GameObject[] Tier3Items;
    public GameObject[] Tier4Items;
    public GameObject[] Tier5Items;

    // % drop variables
    //does a drop occour
    public float DropChance;
    private float RNDDrop;
    private float RNDTier;
    private int RNDItemDrop;

    //if a drop occours what tier is it
    public float Tier1DropChance;
    public float Tier2DropChance;
    public float Tier3DropChance;
    public float Tier4DropChance;
    public float Tier5DropChance;

    // do this stuff when game awakes
    private void Awake()
    {
        instance = this;
    }

    public void Drop(Transform SpawnPos)
    {
        
            // generate a random number between 0 - 100
            RNDDrop = Random.Range(0, 100);

            if (RNDDrop <= DropChance)
        {
            RNDTier = Random.Range(0, (Tier1DropChance + Tier2DropChance + Tier3DropChance + Tier4DropChance + Tier5DropChance));
                // tier 1 drop chance
            if (RNDTier >= 0 && RNDTier <= Tier1DropChance)
            {
                RNDItemDrop = Random.Range(0, Tier1Items.Length);

                //instantiate the dropped object
                Instantiate(Tier1Items[RNDItemDrop], SpawnPos.position, new Quaternion(0,0,0,0), Parent);
            }
                // tier 2 drop chance
            else if (RNDTier > Tier1DropChance && RNDTier <= Tier1DropChance + Tier2DropChance)
            {
                RNDItemDrop = Random.Range(0, Tier2Items.Length);

                //instantiate the dropped object
                Instantiate(Tier2Items[RNDItemDrop], SpawnPos.position, new Quaternion(0, 0, 0, 0), Parent);
            }
                // tier 3 drop chance
            else if (RNDTier > Tier1DropChance + Tier2DropChance && RNDTier <= Tier1DropChance + Tier2DropChance + Tier3DropChance)
            {
                RNDItemDrop = Random.Range(0, Tier3Items.Length);

                //instantiate the dropped object
                Instantiate(Tier3Items[RNDItemDrop], SpawnPos.position, new Quaternion(0, 0, 0, 0), Parent);
            }
                // tier 4 drop chance
            else if (RNDTier > Tier1DropChance + Tier2DropChance + Tier3DropChance && RNDTier <= Tier1DropChance + Tier2DropChance + Tier3DropChance + Tier4DropChance)
            {
                RNDItemDrop = Random.Range(0, Tier4Items.Length);

                //instantiate the dropped object
                Instantiate(Tier4Items[RNDItemDrop], SpawnPos.position, new Quaternion(0, 0, 0, 0), Parent);
            }
                // tier 5 drop chance
            else if (RNDTier > Tier1DropChance + Tier2DropChance + Tier3DropChance + Tier4DropChance && RNDTier <= Tier1DropChance + Tier2DropChance + Tier3DropChance + Tier4DropChance + Tier5DropChance)
            {
                RNDItemDrop = Random.Range(0, Tier5Items.Length);

                //instantiate the dropped object
                Instantiate(Tier5Items[RNDItemDrop], SpawnPos.position, new Quaternion(0, 0, 0, 0), Parent);
            }
        }
        
    }
}
