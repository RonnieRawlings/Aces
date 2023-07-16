// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeckData
{
    #region ATC Variables

    public static int amountShown;

    #endregion

    public static Dictionary<string, bool> cardDeck; // Contains current card selection.

    /// <summary> static constructor <c>DeckData</c> Constructor that fills dictionary data with card data. </summary>
    static DeckData()
    {
        cardDeck = new Dictionary<string, bool>();

        string[] suits = { "Spades", "Hearts", "Clubs", "Diamonds" };
        string[] ranks = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

        // Fills deck with all 52 cards.
        foreach (string suit in suits)
        {
            foreach (string rank in ranks)
            {
                cardDeck.Add(rank + " of " + suit, false);
            }
        }
    }
}
