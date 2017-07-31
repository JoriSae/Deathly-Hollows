﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    List<Item> items = new List<Item>();

    RectOffset padding;
    Vector2 spacing;
    public Vector2 cellSize;

    public Image inventoryBackground;

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

        float x = (slotColumnNumber * cellSize.x) + padding.left + padding.right;
        float y = (slotRowNumber * cellSize.y) + padding.top + padding.bottom;
        inventoryBackground.rectTransform.sizeDelta = new Vector2(x, y);
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

    public void AddItem(Item _item)
    {
        Vector2 gridPosition = CheckGrid(_item);

        if (gridPosition.x != -1 &&
            gridPosition.y != -1)
        {
            for (int y = (int)gridPosition.y; y < (int)gridPosition.y + _item.size.y; ++y)
            {
                for (int x = (int)gridPosition.x; x < (int)gridPosition.x + _item.size.x; ++x)
                {
                    if (slots[x, y].occupied)
                    {
                        slots[x, y].item.currentStack += 1;
                        return;
                    }
                }
            }

            Vector2 itemPosition = slots[(int)gridPosition.x, (int)gridPosition.y].transform.position;

            itemPosition.x -= (cellSize.x / 2);
            itemPosition.y -= (cellSize.y / 2) + (cellSize.y * (_item.size.y - 1));

            Item newItem = Instantiate(_item, itemPosition, Quaternion.identity) as Item;

            newItem.transform.SetParent(itemContainer.transform);

            newItem.imageObject.sprite = newItem.sprite;

            RectTransform rt = newItem.GetComponent<RectTransform>();

            rt.sizeDelta = new Vector2(cellSize.x * newItem.size.x, cellSize.y * newItem.size.y);

            newItem.primaryImage.sizeDelta = new Vector2(cellSize.x * newItem.size.x, cellSize.y * newItem.size.y);

            newItem.secondaryImage.sizeDelta = new Vector2(cellSize.x * newItem.size.x, cellSize.y * newItem.size.y);

            SetOccupied((int)gridPosition.x, (int)gridPosition.y, newItem, true);
        }
    }

    public void SetOccupied(int _xSlot, int _ySlot, Item _item, bool _occupied)
    {
        for (int y = _ySlot; y < _ySlot + _item.size.y; ++y)
        {
            for (int x = _xSlot; x < _xSlot + _item.size.x; ++x)
            {
                slots[x, y].occupied = _occupied;
                slots[x, y].item = _item;
                slots[x, y].xSectionOfItem = x - _xSlot;
                slots[x, y].ySectionOfItem = y - _ySlot;
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
                bool slotOccupied = SlotsOccupiedCheck(ref width, ref height, _item, false);

                if (!slotOccupied)
                {
                    return new Vector2(width, height);
                }
            }
        }

        // Return invalid slot if no available slots
        return new Vector2(-1, -1);
    }

    public bool SlotsOccupiedCheck(ref int _xSlot, ref int _ySlot, Item _item, bool _movingItem)
    {
        // Check if slots are occupied
        if (!slots[_xSlot, _ySlot].occupied)
        {
            // Check if item size exceeds slot row or column amount
            if (_item.size.x + _xSlot > slotColumnNumber ||
                _item.size.y + _ySlot > slotRowNumber)
            {
                // If true continue to next loop
                return true;
            }

            if (_movingItem)
            {
                print("should run");
                // Loop over slots require to hold item
                for (int y = _ySlot; y < _ySlot + (_item.size.y - slots[_xSlot, _ySlot].ySectionOfItem); ++y)
                {
                    for (int x = _xSlot; x < _xSlot + (_item.size.x - slots[_xSlot, _ySlot].xSectionOfItem); ++x)
                    {
                        print("actually runs");
                        // Check if item is null
                        if (slots[x, y].item != null)
                        {
                            print(x + " " + y + " " + _xSlot + " " + _ySlot);
                            print(slots[_xSlot, _ySlot].xSectionOfItem);
                            // Check if current slot is stackable and if not at maximum capacity
                            if (slots[x, y].item.stackable && slots[x, y].item.itemID == _item.itemID &&
                                slots[x, y].item.maxStack > slots[x, y].item.currentStack)
                            {

                                // If true return current slot
                                return false;
                            }
                        }
                    }
                }
            }
            else
            {
                // Check if item is null
                if (slots[_xSlot, _ySlot].item != null)
                {
                    //print(x + " " + y + " " + _xSlot + " " + _ySlot);
                    //print(slots[_xSlot, _ySlot].xSectionOfItem);
                    // Check if current slot is stackable and if not at maximum capacity
                    if (slots[_xSlot, _ySlot].item.stackable && slots[_xSlot, _ySlot].item.itemID == _item.itemID &&
                        slots[_xSlot, _ySlot].item.maxStack > slots[_xSlot, _ySlot].item.currentStack)
                    {

                        // If true return current slot
                        return false;
                    }
                }
            }

            // Loop over slots require to hold item
            for (int y = _ySlot; y < _ySlot + _item.size.y; ++y)
            {
                for (int x = _xSlot; x < _xSlot + _item.size.x; ++x)
                {
                    // Check if item overlaps any other items
                    if (slots[x, y].occupied)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        else
        {
            // Loop over slots require to hold item
            for (int y = _ySlot; y < _ySlot + (_item.size.y - slots[_xSlot, _ySlot].ySectionOfItem); ++y)
            {
                for (int x = _xSlot; x < _xSlot + (_item.size.x - slots[_xSlot, _ySlot].xSectionOfItem); ++x)
                {
                    // Check if item is null
                    if (slots[x, y].item != null)
                    {
                        // Check if current slot is stackable and if not at maximum capacity
                        if (slots[x, y].item.stackable && slots[x, y].item.itemID == _item.itemID &&
                            slots[x, y].item.maxStack > slots[x, y].item.currentStack)
                        {
                            // print(x + " " + y + " " + _xSlot + " " + _ySlot);
                            // print(slots[_xSlot, _ySlot].xSectionOfItem);
                            // If true return current slot
                            return false;
                        }
                    }
                }
            }
        }

        return true;
    }
}
