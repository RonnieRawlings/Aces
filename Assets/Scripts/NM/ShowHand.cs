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

    /// <summary> method <c>ExtractSuitAndRank</c> Extracts the suit & the rank from the given card, return in List<string> format. </summary>
    public List<string> ExtractSuitAndRank(string cardName)
    {
        // Extract the suit and rank from the card name
        string[] words = cardName.Split(' ');
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

        // Returns suit & rank in a list.
        return new List<string>() { suit, rank.ToString() };
    }
    
    /// <summary> method <c>ChangeSuit</c> Changes which suits buttons are enabled. </summary>
    public bool ChangeSuit(List<GameObject> playerHand, int lowestCard, bool isBlack)
    {
        if (lowestCard == int.MaxValue) { Debug.Log("MAX VALUE"); }

        // Iterate through playerHand list      
        for (int i = 0; i < playerHand.Count; i++)
        {
            // Gets suit & rank of current card.
            List<string> cardValues = ExtractSuitAndRank(playerHand[i].GetComponent<Image>().sprite.name);

            if (isBlack)
            {
                // Enable button if card is a black card & the lowest black card
                if ((cardValues[0] == "Spades" || cardValues[0] == "Clubs") && int.Parse(cardValues[1]) == lowestCard)
                {
                    Button button = playerHand[i].GetComponent<Button>();
                    button.interactable = true;

                    return true;
                }
            }
            else
            {
                // Enable button if card is not a black card or not the lowest red card
                if ((cardValues[0] == "Hearts" || cardValues[0] == "Diamonds") && int.Parse(cardValues[1]) == lowestCard)
                {
                    Button button = playerHand[i].GetComponent<Button>();
                    button.interactable = true;

                    return true;
                }
            }
        }

        return false;
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
            // Gets suit & rank of current card.
            List<string> cardValues = ExtractSuitAndRank(card);
            
            // Check if card is black and has a lower rank than current lowestBlackCard
            if ((cardValues[0] == "Spades" || cardValues[0] == "Clubs") && int.Parse(cardValues[1]) < lowestBlackCard)
            {
                lowestBlackCard = int.Parse(cardValues[1]);
            }
        }

        // Find the lowest red card in player's hand
        int lowestRedCard = int.MaxValue;
        foreach (string card in playerHand)
        {
            // Gets suit & rank of current card.
            List<string> cardValues = ExtractSuitAndRank(card);

            // Check if card is red and has a lower rank than current lowestBlackCard
            if ((cardValues[0] == "Hearts" || cardValues[0] == "Diamonds") && int.Parse(cardValues[1]) < lowestRedCard)
            {
                lowestRedCard = int.Parse(cardValues[1]);
            }
        }

        // Calculate spacing between cards
        float cardWidth = 100;
        float spacing = 20;

        // Calculate initial xSpacing value to center middle card
        float totalWidth = playerHand.Count * cardWidth + (playerHand.Count - 1) * spacing;
        float initialX = -totalWidth / 2 + cardWidth / 2;
        float xSpacing = initialX;

        // Gets suit & rank of the latest layed card.
        List<string> latestCard = new List<string>();
        if (!string.IsNullOrEmpty(NMStaticData.latestCard)) { latestCard = ExtractSuitAndRank(NMStaticData.latestCard); }
        
        // Amount of cards buttons disabled.
        int disabledButtons = 0;

        // Iterate through playerHand list
        List<GameObject> createdObjs = new List<GameObject>();
        for (int i = 0; i < playerHand.Count; i++)
        {
            // Gets suit & rank of current card.
            List<string> cardValues = ExtractSuitAndRank(playerHand[i]);         

            // Load the sprite from the Resources folder
            Sprite cardSprite = Resources.Load<Sprite>("Art/Playing Cards/" + cardValues[0] + "/" + playerHand[i]);

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

            if (string.IsNullOrEmpty(NMStaticData.latestCard))
            {
                // Disable button if card is not a black card or not the lowest black card
                if (cardValues[0] != "Spades" && cardValues[0] != "Clubs" || int.Parse(cardValues[1]) != lowestBlackCard)
                {
                    Button button = cardObject.GetComponent<Button>();
                    button.interactable = false;

                    // Increments the db count.
                    disabledButtons++;
                }
            }    
            else
            {               
                // Disables any card that's not the next in the suit.
                if (cardValues[0] != latestCard[0] || int.Parse(cardValues[1]) != int.Parse(latestCard[1]) + 1)
                {
                    // Disables the button.
                    Button button = cardObject.GetComponent<Button>();
                    button.interactable = false;

                    // Increments the db count.
                    disabledButtons++;
                }
            }

            // Gives access to spawned obj.
            createdObjs.Add(cardObject);
        }

        // Calls ChangeSuit when player has reached a dead end.
        if (NMStaticData.latestPlayer == "1" && disabledButtons == playerHand.Count && !NMStaticData.shouldWait)
        {
            if (NMStaticData.latestSuit == "Spades" || NMStaticData.latestSuit == "Clubs")
            {
                // IF no reds, enable lowest black.
                if (!ChangeSuit(createdObjs, lowestRedCard, false))
                {
                    ChangeSuit(createdObjs, lowestBlackCard, true);
                }
                
            }
            else if (NMStaticData.latestSuit == "Hearts" || NMStaticData.latestSuit == "Diamonds")
            {
                // IF no blacks, enable lowest red.
                if (!ChangeSuit(createdObjs, lowestBlackCard, true))
                {
                    ChangeSuit(createdObjs, lowestRedCard, false);
                }                
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