// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardData : MonoBehaviour
{
    public string cardName = "";
    public int cardNum;
    public bool isShown = false;

    void Start()
    {
        // Prevents starting card from changing.
        if (transform.parent.name == "13" && transform.name == "Card 4") { return; }

        // Sets starting settings for other 51 cards.
        this.GetComponent<Button>().targetGraphic = this.GetComponent<Image>();
        this.GetComponent<Button>().interactable = false;

        // Sets colour/alpha to correct starting values.
        ColorBlock colors = this.GetComponent<Button>().colors;
        colors.disabledColor = new Color(colors.disabledColor.r, colors.disabledColor.g, colors.disabledColor.b, 200f / 255f);
        this.GetComponent<Button>().colors = colors;
    }
}
