using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    List<Item> items = new List<Item>();

    RectOffset padding;
    Vector2 spacing;
    Vector2 cellSize;

    public int slotColumnNumber;
    public int slotRowNumber;
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
	
	void Update ()
    {

	}

    public void AddItem()
    {
        Vector2 gridPosition = CheckGrid();

        print(gridPosition.x + " " + gridPosition.y);

        if (gridPosition.x != -1 &&
            gridPosition.y != -1)
        {
            Vector2 itemPosition = slots[(int)gridPosition.x, (int)gridPosition.y].transform.position;
            itemPosition.x -= (cellSize.x / 2);
            itemPosition.y -= (cellSize.y / 2) + (cellSize.y * (item.size.x - 1));

            Item newItem = Instantiate(item, itemPosition, Quaternion.identity) as Item;

            newItem.transform.SetParent(itemContainer.transform);

            for (int x = (int)gridPosition.x; x < (int)(gridPosition.x + item.size.x); ++x)
            {
                for (int y = (int)gridPosition.y; y < (int)(gridPosition.y + item.size.y); ++y)
                {
                    print(x + " " + y);
                    slots[x, y].occupied = true;
                }
            }

        }
    }

    Vector2 CheckGrid()
    {
        for (int width = 0; width < slotRowNumber; ++width)
        {
            for (int height = 0; height < slotColumnNumber; ++height)
            {
                if (!slots[width, height].occupied)
                {
                    if (item.size.x + width > slotRowNumber ||
                        item.size.y + height > slotColumnNumber)
                    {
                        continue;
                    }

                    for (int x = width; x < width + item.size.x; ++x)
                    {
                        for (int y = height; y < height + item.size.y; ++y)
                        {
                            if (slots[x,y].occupied)
                            {
                                goto GridOccupied;
                            }
                        }
                    }

                    return new Vector2 (width, height);

                GridOccupied:
                    continue;
                }
            }
        }

        return new Vector2(-1, -1);
    }
}
