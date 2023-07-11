// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ATCManagement : MonoBehaviour
{
    /// <summary> method <c>SetCardData</c> Sets each CardData obj to a unique card in the 52 card deck. </summary>
    public void SetCardData()
    {
        // Finds cardData objs, creates rand, & gets deck keys.
        CardData[] cards = FindObjectsOfType<CardData>();
        System.Random random = new System.Random();
        List<string> cardKeys = DeckData.cardDeck.Keys.ToList();

        // Shuffles cards using Fisher-Yates algorithm.
        int totalCards = cardKeys.Count;
        while (totalCards > 1)
        {
            // Deincrements + finds rand card.
            totalCards--;
            int randCard = random.Next(totalCards + 1);
            string value = cardKeys[randCard];

            // Swaps current card with rand card.
            cardKeys[randCard] = cardKeys[totalCards];
            cardKeys[totalCards] = value;
        }
        for (int i = 0; i < cards.Length; i++)
        {
            // Assigns shuffled cards to cardData objs.
            cards[i].cardName = cardKeys[i];
            DeckData.cardDeck[cardKeys[i]] = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCardData();
    }
}
