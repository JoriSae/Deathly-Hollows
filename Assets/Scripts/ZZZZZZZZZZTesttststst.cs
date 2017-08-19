using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZZZZZZZZZTesttststst : MonoBehaviour {

    public Inventory inventory;
    public int iiiID;
    public int iiiAmount;

    public void removeItems()
    {
        inventory.RemoveItems(iiiID, iiiAmount);
    }
}
