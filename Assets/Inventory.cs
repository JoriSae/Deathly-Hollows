using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    List<Item> items = new List<Item>();

    RectOffset padding;
    Vector2 spacing;
    Vector2 cellSize;

    public int slotColumnNumber;
    public int slotRowNumber;
    public Slot[,] slots;

    public Slot slot;

	void Start ()
    {
        InitializeGridLayout();
        SpawnInventorySlots();
	}

    void InitializeGridLayout()
    {
        GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();

        padding = gridLayoutGroup.padding;
        cellSize = gridLayoutGroup.cellSize;
        spacing = gridLayoutGroup.spacing;

        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = slotColumnNumber;
    }

    void SpawnInventorySlots()
    {
        slots = new Slot[slotRowNumber, slotColumnNumber];

        for (int width = 0; width < slotRowNumber; ++width)
        {
            for (int height = 0; height < slotColumnNumber; ++height)
            {
                slots[width, height] = Instantiate(slot) as Slot;
                slots[width, height].transform.SetParent(gameObject.transform);
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {

	}
}
