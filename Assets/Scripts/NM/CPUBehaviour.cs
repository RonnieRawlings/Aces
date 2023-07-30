// Author - Ronnie Rawlings.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPUBehaviour : MonoBehaviour
{
    // Places where player can place their tokens.
    [SerializeField] private GameObject middleToken;
    [SerializeField] private List<GameObject> horseTokens;

    // Players starting tokens;
    [SerializeField] private int playerTokens = 10;

    // Management script, players hand, + lay card pos.
    [SerializeField] private NewMarketManagement nm;
    [SerializeField] private List<string> playerHand = new List<string>();
    [SerializeField] private Image layCardPos;

    #region Properties

    /// <summary> property <c>PlayerTokens</c> Allows safe access to the playerTokens var, get & set. </summary>
    public int PlayerTokens
    {
        get { return playerTokens; }
        set { playerTokens = value; }
    }

    #endregion

    /// <summary> method <c>StartingPlay</c> Starts the CPUs play, places starting tokens + chooses a horse. </summary>
    public void StartingPlay()
    {
        // Prevents multiple runs.
        if (middleToken.activeInHierarchy) { return; }

        // Finds correct hand, depends on player name. 
        switch (gameObject.name)
        {
            case "Player 2":
                playerHand = nm.PlayerTwo;
                break;
            case "Player 3":
                playerHand = nm.PlayerThree;
                break;
            case "Player 4":
                playerHand = nm.PlayerFour;
                break;
        }

        // Plays starting tokens.
        playerTokens = playerTokens - 2;
        middleToken.SetActive(true);
        horseTokens[UnityEngine.Random.Range(0, 4)].SetActive(true);
    }

    /// <summary> method <c>ChangeSuit</c> Finds the lowest of the opposite suit colour, lays the card. </summary>
    public void ChangeSuit(string[] ranks)
    {
        if (NMStaticData.latestSuit == "Spades" || NMStaticData.latestSuit == "Clubs")
        {
            // Find the lowest card of the selected randSuit in the CPU's hand
            List<string> cardsOfSelectedSuit = new List<string>();
            foreach (string card in playerHand)
            {
                if (card.EndsWith("Hearts") || card.EndsWith("Diamonds"))
                {
                    cardsOfSelectedSuit.Add(card);
                }
            }
            cardsOfSelectedSuit.Sort((card1, card2) => Array.IndexOf(ranks, card1.Split(' ')[0]).
                CompareTo(Array.IndexOf(ranks, card2.Split(' ')[0])));

            // If red card is found, lay it.
            if (cardsOfSelectedSuit.Count > 0)
            {
                // Lays down the next card.
                StartCoroutine(LayNextCard(cardsOfSelectedSuit[0]));
            }
            else
            {
                // Change suit to black.
                NMStaticData.latestSuit = "Hearts";
                ChangeSuit(ranks);
            }                      
        }
        else
        {
            // Find the lowest card of the selected randSuit in the CPU's hand
            List<string> cardsOfSelectedSuit = new List<string>();
            foreach (string card in playerHand)
            {
                if (card.EndsWith("Clubs") || card.EndsWith("Spades"))
                {
                    cardsOfSelectedSuit.Add(card);
                }
            }
            cardsOfSelectedSuit.Sort((card1, card2) => Array.IndexOf(ranks, card1.Split(' ')[0]).
                CompareTo(Array.IndexOf(ranks, card2.Split(' ')[0])));

            // If red card is found, lay it.
            if (cardsOfSelectedSuit.Count > 0)
            {
                // Lays down the next card.
                StartCoroutine(LayNextCard(cardsOfSelectedSuit[0]));
            }
            else
            {
                // Change suit to red.
                NMStaticData.latestSuit = "Spades";
                ChangeSuit(ranks);
            }
        }
    }

    /// <summary> method <c>CheckForNextCard</c> Spilts latestCard into rank/suit & checks if player hand has the next card. </summary>
    public void CheckForNextCard()
    {
        // Prevents checking when no card has been layed.
        if (string.IsNullOrEmpty(NMStaticData.latestCard) || playerHand.Count == 0) { return; }

        // Define the order of ranks
        string[] ranks = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

        // Split NMStaticData.latestCard into rank and suit
        string[] parts = NMStaticData.latestCard.Split(new string[] { " of " }, System.StringSplitOptions.None);
        string currentRank = parts[0];
        string currentSuit = parts[1];

        // Find the index of the current rank
        int currentRankIndex = System.Array.IndexOf(ranks, currentRank);

        // Check if there is a next rank
        if (currentRankIndex < ranks.Length - 1)
        {
            // Get the next rank
            string nextRank = ranks[currentRankIndex + 1];

            // Create the name of the next card
            string nextCard = nextRank + " of " + currentSuit;

            // Check if CPU contains the next card OR should change suit.
            if (playerHand.Contains(nextCard) && !NMStaticData.shouldWait)
            {
                // Lays down the next card.
                StartCoroutine(LayNextCard(nextCard));
            }
            else if (!NMStaticData.shouldWait && NMStaticData.latestPlayer == this.name.Split(' ')[1])
            {
                if ((!nm.PlayerOne.Contains(nextCard) && !nm.PlayerTwo.Contains(nextCard) && !nm.PlayerThree.Contains(nextCard) && !nm.PlayerFour.Contains(nextCard)) || nm.DummyHand.Contains(nextCard))
                {
                    ChangeSuit(ranks);
                }
            }
        }
        else if (!NMStaticData.shouldWait && NMStaticData.latestPlayer == this.name.Split(' ')[1])
        {
            ChangeSuit(ranks);
        }
    }

    /// <summary> method <c>LayNextCard</c> Places down the provided cardName as the next card. </summary>
    public IEnumerator LayNextCard(string cardName)
    {
        // Prevents multiple laying routines + gives player time to adjust.
        NMStaticData.shouldWait = true;
        yield return new WaitForSeconds(1f);

        // Outline, shows player the card.
        layCardPos.GetComponent<Outline>().enabled = true;

        // Extract the suit from the card name
        string[] words = cardName.Split(' ');
        string suit = words[words.Length - 1];

        // Load the sprite from the Resources folder
        Sprite cardSprite = Resources.Load<Sprite>("Art/Playing Cards/" + suit + "/" + cardName);
        layCardPos.sprite = cardSprite;

        // Wait before placing, gives player time to view.
        yield return new WaitForSeconds(2f);
        layCardPos.GetComponent<Outline>().enabled = false;

        // Removes card from hand.
        switch (gameObject.name)
        {
            case "Player 2":
                nm.PlayerTwo.Remove(cardName);
                break;
            case "Player 3":
                nm.PlayerThree.Remove(cardName);
                break;
            case "Player 4":
                nm.PlayerFour.Remove(cardName);
                break;
        }

        NMStaticData.shouldWait = false; // Allows more laying routines.
    }

    // Start is called before the first frame update
    void Start()
    {
        StartingPlay();
    }

    // Called once at the start of each frame.
    void Update()
    {
        CheckForNextCard();

        if (!nm.StartTokensPlaced) { StartingPlay(); }
    }
}
