// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;

        Button button = transform.gameObject.AddComponent<Button>();
        button.onClick.AddListener(() => GameObject.Find("GameManager").GetComponent<NewMarketManagement>().PlaceCardDown(this.gameObject));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 30, transform.position.z);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.position = initialPosition;
    }
}
