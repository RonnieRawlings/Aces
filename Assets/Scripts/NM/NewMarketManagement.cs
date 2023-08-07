// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewMarketManagement : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private List<string> playerOne, playerTwo, playerThree, playerFour, dummyHand;
    
    #region Player Hand Properties

    /// <summary> property <c>PlayerOne</c> Allows other scripts safe access to the playerOne variable, get & sets. </summary>
    public List<string> PlayerOne
    {
        get { return playerOne; }
        set { playerOne = value; }
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

    /// <summary> property <c>DummyHand</c> Allows other scripts safe access to the dunnyHand variable, get & set. </summary>
    public List<string> DummyHand
    {
        get { return dummyHand; }
        set { dummyHand = value; }
    }

    #endregion

    #region Game Start Vars

    private bool startTokensPlaced = false, hasEnabledHand = false;
    public bool noTokensOne = false, noTokensTwo = false, noTokensThree = false, noTokensFour = false;
    private bool filledPlayerOne = false, filledPlayerTwo = false, filledPlayerThree = false, filledPlayerFour;

    #endregion

    #region End Game Vars

    private bool isEndingRound = false;

    #endregion

    #region Game Start Properties

    /// <summary> property <c>StartTokensPlaced</c> Allows safe access to startTokensPlaced var outside of this script, get & set. </summary>
    public bool StartTokensPlaced
    {
        get { return startTokensPlaced; }
        set { startTokensPlaced = value; }
    }

    /// <summary> property <c>NoTokensOne</c> Allows safe access to noTokensOne var outside of this script, only get. </summary>
    public bool NoTokensOne
    {
        set { noTokensOne = value; }
    }

    /// <summary> property <c>NoTokensTwo</c> Allows safe access to noTokensTwo var outside of this script, only get. </summary>
    public bool NoTokensTwo
    {
        set { noTokensTwo = value; }
    }

    /// <summary> property <c>NoTokensThree</c> Allows safe access to noTokensThree var outside of this script, only get. </summary>
    public bool NoTokensThree
    {
        set { noTokensThree = value; }
    }

    /// <summary> property <c>NoTokensFour</c> Allows safe access to noTokensFour var outside of this script, only get. </summary>
    public bool NoTokensFour
    {
        set { noTokensFour = value; }
    }

    #endregion

    public void SetPlayerData()
    {
        // Initializes players hands lists.
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
            while (true)
            {
                switch (playerIndex)
                {
                    case 0:
                        if (!noTokensOne)
                        {
                            filledPlayerOne = true;

                            playerOne.Add(key);
                            playerIndex = (playerIndex + 1) % 5;
                            goto NextKey;
                        }
                        break;
                    case 1:
                        if (!noTokensTwo)
                        {
                            filledPlayerTwo = true;

                            playerTwo.Add(key);
                            playerIndex = (playerIndex + 1) % 5;
                            goto NextKey;
                        }
                        break;
                    case 2:
                        if (!noTokensThree)
                        {
                            filledPlayerThree = true;

                            playerThree.Add(key);
                            playerIndex = (playerIndex + 1) % 5;
                            goto NextKey;
                        }
                        break;
                    case 3:
                        if (!noTokensFour)
                        {
                            filledPlayerFour = true;

                            playerFour.Add(key);
                            playerIndex = (playerIndex + 1) % 5;
                            goto NextKey;
                        }
                        break;
                    case 4:
                        dummyHand.Add(key);
                        playerIndex = (playerIndex + 1) % 5;
                        goto NextKey;
                }
                playerIndex = (playerIndex + 1) % 5;
            }
        NextKey:;
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

    /// <summary> method <c>EndRound</c> Cleans up table, resets variables. </summary>
    public IEnumerator EndRound()
    {
        // Resets filled vars.
        filledPlayerOne = false;
        filledPlayerTwo = false;
        filledPlayerThree = false;
        filledPlayerFour = false;

        // Reset player hands.
        playerOne.Clear();
        playerTwo.Clear();
        playerThree.Clear();
        playerFour.Clear();

        // Allocates middle token pile.
        canvas.transform.Find("LayedCards").GetComponent<LayedCards>().CollectMiddleTokens();
          
        // Resets horse cards + layed cards.
        canvas.transform.Find("LayedCards").GetComponent<LayedCards>().ResetHorses();
        canvas.transform.Find("LayedCards").GetComponent<LayedCards>().ResetLayedCards();

        // Re-Enable Player token outline/button.
        canvas.transform.Find("Tokens").GetChild(0).GetChild(0).gameObject.SetActive(true);
        canvas.transform.Find("Tokens").GetChild(0).GetComponent<Button>().enabled = true;

        // Disables player hand.
        canvas.transform.Find("Player 1").GetComponent<Button>().interactable = false;
        hasEnabledHand = false;

        // Changes player who is first to lay.
        if (NMStaticData.firstToLay <= 3)
        {
            NMStaticData.firstToLay++;
        }
        else
        {
            NMStaticData.firstToLay = 1;
        }

        // Re-Enables starting token placement.
        startTokensPlaced = false;

        // Resets roundEnd var.
        isEndingRound = false;

        // Gives player new random hand.
        yield return new WaitForSeconds(0.2f);
        SetPlayerData();
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
            canvas.transform.GetChild(canvas.transform.childCount - 3).GetComponent<Button>().interactable = true;
            hasEnabledHand = true;
        }

        // Checks if a player has finished their hand, if so ends the round.
        if ( ((playerOne.Count == 0 && filledPlayerOne) || (playerTwo.Count == 0 && filledPlayerTwo) || (playerThree.Count == 0 && filledPlayerThree) || 
            (playerFour.Count == 0 && filledPlayerFour)) && !isEndingRound)
        {
            // Ends the roun + prevents multiple methods being called.
            isEndingRound = true;
            StartCoroutine(EndRound());            
        }
    }
}
