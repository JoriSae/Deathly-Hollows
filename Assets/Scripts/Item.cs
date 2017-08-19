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

    public bool equipable;

    public Transform topLeftPivotPoint;
    public Image equipableItemSlot;

    public Vector2 size;
    public Vector2 gridPosition;

    private Vector2 oldPosition;
    private Vector2 oldTopLeft;

    private Inventory inventory;
    private Image equip;
    private Slot equipSlot;

    public Text numberOfStacksText;

    private Player player;

    [Header("Sprite to use & Object to apply to")]
    public Image imageObject;
    public Sprite sprite;

    [Header("RectTransforms")]
    public RectTransform primaryImage;
    public RectTransform secondaryImage;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        equip = GameObject.Find("Equip").GetComponent<Image>();
        equipSlot = equip.GetComponent<Slot>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        UpdateState();
        UpdateText();
        CheckCurrentStack();
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

            if (topLeftPivotPoint.position.x + (inventory.cellSize.x * size.x) > equip.transform.position.x - (equip.rectTransform.sizeDelta.x / 2) &&
                topLeftPivotPoint.position.x < equip.transform.position.x + (equip.rectTransform.sizeDelta.x / 2) &&
                topLeftPivotPoint.position.y - (inventory.cellSize.y * size.y) < equip.transform.position.y + (equip.rectTransform.sizeDelta.y / 2) &&
                topLeftPivotPoint.position.y > equip.transform.position.y - (equip.rectTransform.sizeDelta.y / 2))
            {
                equipSlot.occupied = false;
            }
        }
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            itemState = ItemState.itemStationary;

            if (topLeftPivotPoint.position.x + (inventory.cellSize.x * size.x) > equip.transform.position.x - (equip.rectTransform.sizeDelta.x / 2) &&
                topLeftPivotPoint.position.x                                   < equip.transform.position.x + (equip.rectTransform.sizeDelta.x / 2) &&
                topLeftPivotPoint.position.y - (inventory.cellSize.y * size.y) < equip.transform.position.y + (equip.rectTransform.sizeDelta.y / 2) &&
                topLeftPivotPoint.position.y                                   > equip.transform.position.y - (equip.rectTransform.sizeDelta.y / 2))
            {
                if (!equipSlot.occupied)
                {
                    transform.position = equip.transform.position;
                    equipSlot.occupied = true;

                    Vector2 itemPosition = equip.transform.position;

                    itemPosition.x -= ((inventory.cellSize.x / 2) * size.x);
                    itemPosition.y -= ((inventory.cellSize.y / 2) * size.y);

                    print((size.y > 1 ? 1 : 0));

                    gridPosition = new Vector2(-1, -1);

                    gameObject.transform.position = itemPosition;

                    switch (stringID)
                    {
                        case "Sword":
                            print("check");
                            player.ChangeWeapon(1);
                            break;
                        case "Bow":
                            player.ChangeWeapon(0);
                            break;
                    }

                    return;
                }

                goto bad;
            }

                if (topLeftPivotPoint.position.x + (inventory.cellSize.x * size.x) < inventory.inventoryBackground.transform.position.x - (inventory.inventoryBackground.rectTransform.sizeDelta.x / 2) ||
                    topLeftPivotPoint.position.x                                   > inventory.inventoryBackground.transform.position.x + (inventory.inventoryBackground.rectTransform.sizeDelta.x / 2) ||
                    topLeftPivotPoint.position.y - (inventory.cellSize.y * size.y) > inventory.inventoryBackground.transform.position.y + (inventory.inventoryBackground.rectTransform.sizeDelta.y / 2) ||
                    topLeftPivotPoint.position.y                                   < inventory.inventoryBackground.transform.position.y - (inventory.inventoryBackground.rectTransform.sizeDelta.y / 2))
            {
                Cursor.visible = true;

                print(gridPosition);
                //inventory.SetOccupied((int)gridPosition.x, (int)gridPosition.y, this, false);
                Destroy(gameObject);

                return;
            }

            for (int height = 0; height < inventory.slotRowNumber; height++)
            {
                for (int width = 0; width < inventory.slotColumnNumber; width++)
                {
                    if (topLeftPivotPoint.position.x + (inventory.cellSize.x / 2) >= inventory.slots[width, height].transform.position.x - (inventory.cellSize.x / 2) &&
                        topLeftPivotPoint.position.x + (inventory.cellSize.x / 2) < inventory.slots[width, height].transform.position.x + (inventory.cellSize.x / 2) &&
                        topLeftPivotPoint.position.y - (inventory.cellSize.y / 2) >= inventory.slots[width, height].transform.position.y - (inventory.cellSize.y / 2) &&
                        topLeftPivotPoint.position.y - (inventory.cellSize.y / 2) < inventory.slots[width, height].transform.position.y + (inventory.cellSize.y / 2))
                    {
                        bool slotOccupied = inventory.SlotsOccupiedCheck(ref width, ref height, this, true);

                        Slot slot = inventory.slots[width, height];

                        if (!slotOccupied)
                        {
                            for (int y = height; y < height + size.y; ++y)
                            {
                                for (int x = width; x < width + size.x; ++x)
                                {
                                    slot = inventory.slots[x, y];
                                    if (slot.occupied && inventory.slots[x, y].item.itemID == itemID)
                                    {
                                        print(x + " " + y);
                                        goto occupied;
                                    }
                                }
                            }
                            if (slot.occupied)
                            {
                                goto bad;
                            }

                            occupied:
                            if (slot.occupied)
                            {
                                if (slot.item.currentStack + currentStack > maxStack)
                                {
                                    currentStack = (slot.item.currentStack + currentStack) - maxStack;
                                    slot.item.currentStack = maxStack;
                                }
                                else
                                {
                                    slot.item.currentStack += currentStack;

                                    Destroy(gameObject);

                                    return;
                                }
                            }
                            else
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
            }

            bad:
            if (gridPosition.x > -1)
            {
                inventory.SetOccupied((int)gridPosition.x, (int)gridPosition.y, this, true);
                print("not becoming true again");
            }
            else if (equipSlot.occupied)
            {
                equipSlot.occupied = true;
            }

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

    void CheckCurrentStack()
    {
        if (currentStack <= 0)
        {
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
                        Destroy(gameObject);
                    }
                }
            }
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