// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewMarketManagement : MonoBehaviour
{
    [SerializeField] private List<string> playerOne, playerTwo, playerThree, playerFour, dummyHand;

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

    // Start is called before the first frame update
    void Start()
    {
        SetPlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
