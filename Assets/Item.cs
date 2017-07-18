using UnityEngine;
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
    public int maxStack;

    public Transform topLeftPivotPoint;

    public Vector2 size;
    public Vector2 gridPosition;

    private Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        UpdateState();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            offset = gameObject.transform.position - Input.mousePosition;
            itemState = ItemState.itemDrag;
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
                    if (topLeftPivotPoint.position.x >= inventory.slots[width, height].transform.position.x - (inventory.cellSize.x / 2) &&
                        topLeftPivotPoint.position.x < inventory.slots[width, height].transform.position.x + (inventory.cellSize.x / 2) &&
                        topLeftPivotPoint.position.y >= inventory.slots[width, height].transform.position.y - (inventory.cellSize.y / 2) &&
                        topLeftPivotPoint.position.y < inventory.slots[width, height].transform.position.y + (inventory.cellSize.y / 2))
                    {
                        Vector2 itemPosition = inventory.slots[width, height].transform.position;

                        print(width + " " + height);

                        itemPosition.x -= (inventory.cellSize.x / 2);
                        itemPosition.y -= (inventory.cellSize.y / 2) + (inventory.cellSize.y * (size.y - 1));

                        gameObject.transform.position = itemPosition;
                    }
                }
            }

            //print(topLeftPivotPoint.position);
            //
            //Vector2 gridPos = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y) - new Vector2(-400, 100);
            //gridPos /= 100.0f;
            //gridPos.y = -gridPos.y;
            //gridPos += new Vector2(0.5f, 0.5f);
            //gridPos.x = (int)gridPos.x;
            //gridPos.y = (int)gridPos.y;
            //gridPos.x *= 100.0f+-400.0f;
            //gridPos.y *= -100.0f + 100.0f;
            //gameObject.transform.localPosition = new Vector3(gridPos.x, gridPos.y, 0);

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