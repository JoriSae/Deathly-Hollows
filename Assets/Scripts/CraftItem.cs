using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftItem : MonoBehaviour
{

    public int StoneRequired;
    public int WoodRequired;

    public int stoneAmount;
    public int woodAmount;

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
        if (stoneAmount <= StoneRequired && woodAmount <= WoodRequired)
        {
            GetComponentInChildren<Button>().interactable = false;
        }

        if (stoneAmount >= StoneRequired && woodAmount >= WoodRequired)
        {
            GetComponentInChildren<Button>().interactable = true;
        }
    }

    public void craftItem()
    {
        foreach (Item i in inventory.items)
        {
            if (i.itemID == 0)
                stoneAmount += i.currentStack;

            if (i.itemID == 2)
                woodAmount += i.currentStack;
        }

        if (stoneAmount >= StoneRequired && woodAmount >= WoodRequired)
        {
            inventory.AddItem(itemToAdd);
            stoneAmount -= StoneRequired;
            woodAmount -= WoodRequired;
        }
    }
}
