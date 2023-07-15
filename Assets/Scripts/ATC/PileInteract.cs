// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PileInteract : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CardData cardData = collision.GetComponent<CardData>();

        if (collision.CompareTag("draggableUI") && cardData.cardNum.ToString() == this.name && cardData.isShown)
        {
            collision.transform.GetComponent<UIDraggable>().enabled = false;

            collision.transform.parent = transform;
            collision.transform.rotation = transform.rotation;

            collision.transform.position = transform.GetChild(0).position + (transform.up * 30);
            collision.transform.SetAsFirstSibling();

            // Searches through every child pile.
            foreach (Transform child in GameObject.Find("Canvas").transform.GetChild(1))
            {
                // If correct pile found, enable outline + button interact.
                if (child.name == cardData.cardNum.ToString())
                {
                    child.GetChild(child.childCount - 1).GetComponent<Button>().interactable = true;                    
                }
                else
                {

                }
            }
        }
    }
}
