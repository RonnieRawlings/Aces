using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileInteract : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("draggableUI") && collision.GetComponent<CardData>().cardNum.ToString() == this.name)
        {
            collision.transform.GetComponent<UIDraggable>().enabled = false;

            collision.transform.parent = transform;
            collision.transform.rotation = transform.rotation;

            collision.transform.position = transform.GetChild(0).position + (transform.up * 30);
            collision.transform.SetAsFirstSibling();
        }
    }
}
