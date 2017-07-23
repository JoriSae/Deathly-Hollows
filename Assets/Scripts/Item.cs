﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    enum ItemState { itemStationary, itemDrag }
    ItemState itemState;

    Vector3 offset;

    public string stringID;
    public int itemID;

    public bool stackable;
    public int currentStack;
    public int maxStack;

    public Transform topLeftPivotPoint;

    public Vector2 size;
    public Vector2 gridPosition;

    private Vector2 oldPosition;
    private Vector2 oldTopLeft;

    private Inventory inventory;

    public Text numberOfStacksText;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        UpdateState();
        UpdateText();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            oldPosition = transform.position;
            offset = gameObject.transform.position - Input.mousePosition;
            itemState = ItemState.itemDrag;

            for (int height = 0; height < inventory.slotRowNumber; height++)
            {
                for (int width = 0; width < inventory.slotColumnNumber; width++)
                {
                    if (topLeftPivotPoint.position.x + (inventory.cellSize.x / 2) == inventory.slots[width, height].transform.position.x &&
                        topLeftPivotPoint.position.y - (inventory.cellSize.x / 2) == inventory.slots[width, height].transform.position.y)
                    {
                        print(inventory.slots[width, height].transform.position + " " + topLeftPivotPoint.position);

                        gridPosition = new Vector2(width, height);
                        inventory.SetOccupied(width, height, this, false);
                    }
                }
            }
        }
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            itemState = ItemState.itemStationary;

            for (int height = 0; height < inventory.slotRowNumber; height++)
            {
                for (int width = 0; width < inventory.slotColumnNumber; width++)
                {
                    if (topLeftPivotPoint.position.x + (inventory.cellSize.x / 2) >= inventory.slots[width, height].transform.position.x - (inventory.cellSize.x / 2) &&
                        topLeftPivotPoint.position.x + (inventory.cellSize.x / 2) < inventory.slots[width, height].transform.position.x + (inventory.cellSize.x / 2) &&
                        topLeftPivotPoint.position.y - (inventory.cellSize.y / 2) >= inventory.slots[width, height].transform.position.y - (inventory.cellSize.y / 2) &&
                        topLeftPivotPoint.position.y - (inventory.cellSize.y / 2) < inventory.slots[width, height].transform.position.y + (inventory.cellSize.y / 2))
                    {
                        bool slotOccupied = inventory.SlotsOccupiedCheck(ref width, ref height, this);

                        if (!slotOccupied)
                        {
                            inventory.SetOccupied(width, height, this, true);

                            Vector2 itemPosition = inventory.slots[width, height].transform.position;

                            itemPosition.x -= (inventory.cellSize.x / 2);
                            itemPosition.y -= (inventory.cellSize.y / 2) + (inventory.cellSize.y * (size.y - 1));

                            gameObject.transform.position = itemPosition;

                            return;
                        }
                    }
                }
            }

            inventory.SetOccupied((int)gridPosition.x, (int)gridPosition.y, this, true);
            gameObject.transform.position = oldPosition;
        }
    }

    void UpdateText()
    {
        if (currentStack > 1)
        {
            numberOfStacksText.text = currentStack.ToString();
        }
        else
        {
            numberOfStacksText.text = "";
        }
    }

    void UpdateState()
    {
        switch (itemState)
        {
            case ItemState.itemDrag:
                gameObject.transform.position = Input.mousePosition + offset;
                Cursor.visible = false;
                break;
            case ItemState.itemStationary:
                Cursor.visible = true;
                break;
        }
    }
}