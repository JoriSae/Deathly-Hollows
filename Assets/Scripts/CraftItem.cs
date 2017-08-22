using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftItem : MonoBehaviour
{

    public int StoneRequired;
    public int WoodRequired;
    public int ThatchRequired;

    public int StoneID;
    public int WoodID;
    public int ThatchID;

    public int stoneAmount;
    public int woodAmount;
    public int thatchAmount;

    public GameObject removeItems;

    public Item itemToAdd;

    //public GameObject CorrectAmount;
    //public GameObject WrongAmount;

    Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    private void Update()
    {
        foreach (Item i in inventory.items)
        {
            if (i.itemID == StoneID)
                stoneAmount = i.currentStack;

            if (i.itemID == WoodID)
                woodAmount = i.currentStack;

            if (i.itemID == ThatchID)
                thatchAmount = i.currentStack;
        }

        if (stoneAmount < StoneRequired || woodAmount < WoodRequired || thatchAmount < ThatchRequired)
        {
            GetComponentInChildren<Button>().interactable = false;
        }

        if (stoneAmount >= StoneRequired && woodAmount >= WoodRequired && thatchAmount >= ThatchRequired)
        {
            GetComponentInChildren<Button>().interactable = true;
        }


    }

    public void craftItem()
    {
        if (itemToAdd.tag == "Melee")
        {
            itemToAdd.GetComponent<Equipable>().OnCreation(Equipable.WeaponType.Melee);
        }

        if (itemToAdd.tag == "Ranged")
        {
            itemToAdd.GetComponent<Equipable>().OnCreation(Equipable.WeaponType.Ranged);
        }

        if (stoneAmount >= StoneRequired && thatchAmount >= ThatchRequired && woodAmount >= WoodRequired)
        {
            inventory.AddItem(itemToAdd);
            stoneAmount -= StoneRequired;
            woodAmount -= WoodRequired;
            thatchAmount -= thatchAmount;

            inventory.RemoveItems(StoneID, StoneRequired);
            inventory.RemoveItems(WoodID, WoodRequired);
            inventory.RemoveItems(ThatchID, ThatchRequired);
        }
    }
}
