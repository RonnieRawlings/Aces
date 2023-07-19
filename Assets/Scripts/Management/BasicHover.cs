// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BasicHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI gameTitle;

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameTitle.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameTitle.enabled = false;
    }
}
