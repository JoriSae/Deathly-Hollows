using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject inventoryUI;
    public GameObject crafting;
    private bool inventoryActive = false;
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryActive = !inventoryActive;

            if (crafting.activeSelf == false)
                crafting.SetActive(true);
            else if (crafting.activeSelf == true)
                crafting.SetActive(false);

            if (inventoryActive)
            {
                inventoryUI.GetComponent<RectTransform>().localPosition = Vector2.zero;
            }
            else
            {
                inventoryUI.transform.position = Vector2.one * 10000;
            }
        }
    }

    public void InventoryButtonActivation()
    {
        inventoryActive = !inventoryActive;

        if (crafting.activeSelf == false)
            crafting.SetActive(true);
        else if (crafting.activeSelf == true)
            crafting.SetActive(false);

        if (inventoryActive)
        {
            inventoryUI.GetComponent<RectTransform>().localPosition = Vector2.zero;
        }
        else
        {
            inventoryUI.transform.position = Vector2.one * 10000;
        }
    }

}
