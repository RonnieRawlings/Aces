// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    /// <summary> method <c>SwitchCardUp</c> Changes image sprite to face up card, disables outline. </summary>
    public void SwitchCardUp(CardData cardData)
    {
        // Get the Image component attached to this object
        Image image = cardData.gameObject.GetComponent<Image>();

        // Extract the suit from the card name
        string[] words = cardData.cardName.Split(' ');
        string suit = words[words.Length - 1];

        // Load the sprite from the Resources folder
        Sprite sprite = Resources.Load<Sprite>("Art/Playing Cards/" + suit + "/" + cardData.cardName);

        // Set the sprite of the Image component
        image.sprite = sprite;

        // Disables outline.
        cardData.gameObject.GetComponent<Outline>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCardData();
    }
}
