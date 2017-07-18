using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    List<Item> items = new List<Item>();

    RectOffset padding;
    Vector2 spacing;
    public Vector2 cellSize;

    public int slotRowNumber;
    public int slotColumnNumber;
    public Slot[,] slots;

    public Slot slot;

    public Item item;

    public GameObject itemContainer;

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
        slots = new Slot[slotColumnNumber, slotRowNumber];

        for (int height = 0; height < slotRowNumber; ++height)
        {
            for (int width = 0; width < slotColumnNumber; ++width)
            {
                slots[width, height] = Instantiate(slot) as Slot;
                slots[width, height].transform.SetParent(gameObject.transform);
            }
        }
    }
	
	void Update ()
    {

	}

    public void AddItem(Item _item)
    {
        Vector2 gridPosition = CheckGrid(_item);

        if (gridPosition.x != -1 &&
            gridPosition.y != -1)
        {

            //print(gridPosition);
            Vector2 itemPosition = slots[(int)gridPosition.x, (int)gridPosition.y].transform.position;

            itemPosition.x -= (cellSize.x / 2);
            itemPosition.y -= (cellSize.y / 2) + (cellSize.y * (_item.size.y - 1));

            Item newItem = Instantiate(_item, itemPosition, Quaternion.identity) as Item;

            newItem.transform.SetParent(itemContainer.transform);

            for (int y = (int)gridPosition.y; y < (int)(gridPosition.y + _item.size.y); ++y)
            {
                for (int x = (int)gridPosition.x; x < (int)(gridPosition.x + _item.size.x); ++x)
                {
                    //print(x + " " + y);
                    slots[x, y].occupied = true;
                    slots[x, y].item = _item;
                }
            }

        }
    }

    Vector2 CheckGrid(Item _item)
    {
        // Loop through all slots
        for (int height = 0; height < slotRowNumber; ++height)
        {
            for (int width = 0; width < slotColumnNumber; ++width)
            {
                // Check if slots are occupied
                if (!slots[width, height].occupied)
                {
                    // Check if item size exceeds slot row or column amount
                    if (_item.size.x + width > slotColumnNumber ||
                        _item.size.y + height > slotRowNumber)
                    {
                        // If true continue to next loop
                        continue;
                    }

                    // Loop over slots require to hold item
                    for (int y = height; y < height + _item.size.y; ++y)
                    {
                        for (int x = width; x < width + _item.size.x; ++x)
                        {
                            print("width: " + width + " height: " + height + " x: " + x + " y: " + y + " item size: " + _item.size);

                            // Check if item overlaps any other items
                            if (slots[x, y].occupied)
                            {
                                goto GridOccupied;
                            }
                        }
                    }

                    //print("##############Found: " + width + " " + height);

                    // Retrun current slot
                    return new Vector2(width, height);

                GridOccupied:
                    continue;
                }
                else
                {
                    // Check if item is null
                    if (slots[width, height].item != null)
                    {
                        // Check if current slot is stackable and if not at maximum capacity
                        if (slots[width, height].item.stackable &&
                            slots[width, height].item.maxStack > slots[width, height].currentStack)
                        {
                            // If true return current slot
                            return new Vector2(width, height);
                        }

                        // If slot is occupied and not stackable, check if the item exceeds the size of 1 and if the item exceeds the boundary
                        if ((int)slots[width, height].item.size.y > 1 &&
                            slots[width, height].item.size.y + width > slotColumnNumber)
                        {

                        }
                    }
                }

                //print("Not Found: " + width + " " + height);
            }
        }

        // Return invalid slot if no available slots
        return new Vector2(-1, -1);
    }
}
