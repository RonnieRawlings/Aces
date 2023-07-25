// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NewMarketManagement : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private List<string> playerOne, playerTwo, playerThree, playerFour, dummyHand;
    
    #region Player Hand Properties

    /// <summary> property <c>PlayerOne</c> Allows other scripts safe access to the playerOne variable, only get. </summary>
    public List<string> PlayerOne
    {
        get { return playerOne; }
    }

    /// <summary> property <c>PlayerTwo</c> Allows other scripts safe access to the playerTwo variable, get & set. </summary>
    public List<string> PlayerTwo
    {
        get { return playerTwo; }
        set { playerTwo = value; }
    }

    /// <summary> property <c>PlayerThree</c> Allows other scripts safe access to the playerThree variable, get & set. </summary>
    public List<string> PlayerThree
    {
        get { return playerThree; }
        set { playerThree = value; }
    }

    /// <summary> property <c>PlayerFour</c> Allows other scripts safe access to the playerFour variable, get & set. </summary>
    public List<string> PlayerFour
    {
        get { return playerFour; }
        set { playerFour = value; }
    }

    /// <summary> property <c>DummyHand</c> Allows other scripts safe access to the dunnyHand variable, only get. </summary>
    public List<string> DummyHand
    {
        get { return dummyHand; }
    }

    #endregion

    #region Game Start Vars

    private bool startTokensPlaced = false, hasEnabledHand = false;

    #endregion

    #region Game Start Properties

    /// <summary> property <c>StartTokensPlaced</c> Allows safe access to startTokensPlaced var outside of this script, only set. </summary>
    public bool StartTokensPlaced
    {
        set { startTokensPlaced = value; }
    }

    #endregion

    public void SetPlayerData()
    {
        // Initilises players hands lists.
        playerOne = new List<string>();
        playerTwo = new List<string>();
        playerThree = new List<string>();
        playerFour = new List<string>();
        dummyHand = new List<string>();

        // Create a list of keys from the DeckData.cardDeck dictionary
        List<string> cardKeys = new List<string>(DeckData.cardDeck.Keys);

        // Shuffle the cardKeys list
        System.Random rnd = new System.Random();
        cardKeys = cardKeys.OrderBy(x => rnd.Next()).ToList();

        // Distribute the cards to the players
        int playerIndex = 0;
        foreach (string key in cardKeys)
        {
            switch (playerIndex)
            {
                case 0:
                    playerOne.Add(key);
                    break;
                case 1:
                    playerTwo.Add(key);
                    break;
                case 2:
                    playerThree.Add(key);
                    break;
                case 3:
                    playerFour.Add(key);
                    break;
                case 4:
                    dummyHand.Add(key);
                    break;
            }
            playerIndex = (playerIndex + 1) % 5;
        }

        OrderHands(); // Puts each hand in the correct starting order.
    }

    /// <summary> method <c>OrderHands</c> Eaiser way to order each hand, calls the OrderHand method for each hand. </summary>
    public void OrderHands()
    {
        playerOne = OrderHand(playerOne);
        playerTwo = OrderHand(playerTwo);
        playerThree = OrderHand(playerThree);
        playerFour = OrderHand(playerFour);
        dummyHand = OrderHand(dummyHand);
    }

    /// <summary> method <c>OrderHand</c> Takes a list as input & uses Linq to sort the list by suit then rank. </summary>
    private List<string> OrderHand(List<string> hand)
    {
        var suitOrder = new List<string> { "Hearts", "Diamonds", "Spades", "Clubs" };
        var rankOrder = new List<string> { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

        return hand.OrderBy(card => suitOrder.IndexOf(card.Split(' ')[2]))
                   .ThenBy(card => rankOrder.IndexOf(card.Split(' ')[0]))
                   .ToList();
    }

    /// <summary> method <c>ClickEvent</c> Places the card down in the pile & removes the card from players deck/hand. 
    public void PlaceCardDown(GameObject card)
    {
        // Gets card image, name, & removes card obj.
        Sprite replacementSprite = card.GetComponent<Image>().sprite;
        string cardName = replacementSprite.name;

        // Allows hand to be re-enabled + removes all shown cards.
        card.transform.parent.GetComponent<ShowHand>().HandShown = false;
        foreach (Transform child in card.transform.parent)
        {
            Destroy(child.gameObject);
        }

        // Replaces sprite.
        Image placeImage = canvas.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        placeImage.sprite = replacementSprite;

        playerOne.Remove(cardName); // Removes card from players hand.
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPlayerData();
    }

    // Called once per frame.
    private void Update()
    {
        // Enables player hand when tokens are placed.
        if (startTokensPlaced && !hasEnabledHand)
        {
            canvas.transform.GetChild(canvas.transform.childCount - 1).GetComponent<Button>().interactable = true;
            hasEnabledHand = true;
        }
    }
}
