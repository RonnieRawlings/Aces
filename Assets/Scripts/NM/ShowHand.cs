// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHand : MonoBehaviour
{
    // Decides if hand needs to be shown.
    [SerializeField] private bool handShown = false;

    #region Properties

    /// <summary> property <c>HandShown</c> Allows safe access to the handShown variable for outside scripts, only set. </summary>
    public bool HandShown
    {
        set { handShown = value; }
    }

    #endregion

    /// <summary> method <c>ChooseHandMethod</c> Uses handShown bool to decide which method should be called. </summary>
    public void ChooseHandMethod()
    {
        // Closes/shows card hand depending on handShown.
        if (handShown)
        {
            ClosePlayerHand();
            handShown = false;
        }
        else
        {
            ShowPlayerHand();
            handShown = true;
        }
    }

    /// <summary> method <c>ShowPlayerHand</c> Shows the players current hand on button press. </summary>
    public void ShowPlayerHand()
    {
        // Gets access to players hand.
        List<string> playerHand = GameObject.Find("GameManager").GetComponent<NewMarketManagement>().PlayerOne;

        // Find the lowest black card in player's hand
        int lowestBlackCard = int.MaxValue;
        foreach (string card in playerHand)
        {
            // Extract the suit and rank from the card name
            string[] words = card.Split(' ');
            string suit = words[words.Length - 1];
            int rank;

            // Prevents string formatting error.
            if (words[0] == "Ace")
                rank = 14;
            else if (words[0] == "Jack")
                rank = 11;
            else if (words[0] == "Queen")
                rank = 12;
            else if (words[0] == "King")
                rank = 13;
            else
                rank = int.Parse(words[0]);

            // Check if card is black and has a lower rank than current lowestBlackCard
            if ((suit == "Spades" || suit == "Clubs") && rank < lowestBlackCard)
            {
                lowestBlackCard = rank;
            }
        }

        // Calculate spacing between cards
        float cardWidth = 100;
        float spacing = 20;

        // Calculate initial xSpacing value to center middle card
        float totalWidth = playerHand.Count * cardWidth + (playerHand.Count - 1) * spacing;
        float initialX = -totalWidth / 2 + cardWidth / 2;
        float xSpacing = initialX;

        // Iterate through playerHand list
        for (int i = 0; i < playerHand.Count; i++)
        {
            // Extract the suit and rank from the card name
            string[] words = playerHand[i].Split(' ');
            string suit = words[words.Length - 1];
            int rank;

            // Prevents string formatting error.
            if (words[0] == "Ace")
                rank = 14;
            else if (words[0] == "Jack")
                rank = 11;
            else if (words[0] == "Queen")
                rank = 12;
            else if (words[0] == "King")
                rank = 13;
            else
                rank = int.Parse(words[0]);

            // Load the sprite from the Resources folder
            Sprite cardSprite = Resources.Load<Sprite>("Art/Playing Cards/" + suit + "/" + playerHand[i]);

            // Create new GameObject
            GameObject cardObject = new GameObject(playerHand[i]);

            // Add Image component and set sprite
            Image image = cardObject.AddComponent<Image>();
            image.sprite = cardSprite;

            // Set RectTransform properties
            RectTransform rectTransform = cardObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(cardWidth, 140);

            // Set as child of current GameObject
            cardObject.transform.SetParent(transform, false);

            // Set anchored position
            rectTransform.localPosition = new Vector2(xSpacing, rectTransform.sizeDelta.y * 1);

            // Increment xSpacing for next card
            xSpacing += cardWidth + spacing;

            // Add CardHover script to obj.
            cardObject.AddComponent<CardHover>();

            // Disable button if card is not a black card or not the lowest black card
            if (suit != "Spades" && suit != "Clubs" || rank != lowestBlackCard)
            {
                Button button = cardObject.GetComponent<Button>();
                button.interactable = false;
            }
        }
    }

    /// <summary> method <c>ClosePlayerHand</c> Allows the player to close their hand early, without placing a card. </summary>
    public void ClosePlayerHand()
    {
        // Removes all card visuals.
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}