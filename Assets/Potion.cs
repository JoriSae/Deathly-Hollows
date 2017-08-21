using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Potion : MonoBehaviour, IPointerUpHandler
{

    public float HealAmount;

	// Use this for initialization
	void Start () {
		
	}

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Health + HealAmount > GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MaxHealth)
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Health = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MaxHealth;

            else
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Health += HealAmount;
        }
    }
}
