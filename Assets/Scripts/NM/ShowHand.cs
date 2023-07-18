// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHand : MonoBehaviour
{
    /// <summary> method <c>ShowPlayerHand</c> Shows the players current hand on button press. </summary>
    public void ShowPlayerHand()
    {
        // Gets access to players hand.
        List<string> playerHand = GameObject.Find("GameManager").GetComponent<NewMarketManagement>().PlayerOne;

        // Calculate spacing between cards
        float cardWidth = 100; // Width of a card
        float spacing = 20; // Spacing between cards

        // Calculate initial xSpacing value to center middle card
        float totalWidth = playerHand.Count * cardWidth + (playerHand.Count - 1) * spacing;
        float initialX = -totalWidth / 2 + cardWidth / 2;
        float xSpacing = initialX;

        // Iterate through playerHand list
        for (int i = 0; i < playerHand.Count; i++)
        {
            // Extract the suit from the card name
            string[] words = playerHand[i].Split(' ');
            string suit = words[words.Length - 1];

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
        }

        GetComponent<Button>().interactable = false;
    }
}
