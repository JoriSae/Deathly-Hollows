using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    enum ItemState { itemStationary, itemDrag }
    ItemState itemState;

    Vector3 offset;

    string stringID;
    int itemID;

    public Vector2 size;
    public Vector2 gridPosition;

    void Update()
    {
        UpdateState();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        offset = gameObject.transform.position - Input.mousePosition;
        itemState = ItemState.itemDrag;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        itemState = ItemState.itemStationary;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Slot"))
        {
            if (itemState == ItemState.itemStationary)
            {
                if (transform.position.x > collision.transform.position.x - 50 &&
                    transform.position.x <= collision.transform.position.x + 50 &&
                    transform.position.y > collision.transform.position.y - 50 &&
                    transform.position.y <= collision.transform.position.y + 50)
                {
                    transform.position = collision.transform.position;
                }
            }
        }
    }
}