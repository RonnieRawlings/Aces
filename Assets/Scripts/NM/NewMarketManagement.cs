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
