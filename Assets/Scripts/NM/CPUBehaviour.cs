// Author - Ronnie Rawlings.

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

    /// <summary> method <c>StartingPlay</c> Starts the CPUs play, places starting tokens + chooses a horse. </summary>
    public void StartingPlay()
    {
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
        horseTokens[Random.Range(0, 4)].SetActive(true);
    }

    /// <summary> method <c>CheckForNextCard</c> Spilts latestCard into rank/suit & checks if player hand has the next card. </summary>
    public void CheckForNextCard()
    {
        // Prevents checking when no card has been layed.
        if (string.IsNullOrEmpty(NMStaticData.latestCard)) { return; }

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

            // Check if playerOne contains the next card
            if (playerHand.Contains(nextCard))
            {
                // Lays down the next card.
                LayNextCard(nextCard);
            }
        }
    }

    /// <summary> method <c>LayNextCard</c> Places down the provided cardName as the next card. </summary>
    public void LayNextCard(string cardName)
    {
        // Extract the suit from the card name
        string[] words = cardName.Split(' ');
        string suit = words[words.Length - 1];

        // Load the sprite from the Resources folder
        Sprite cardSprite = Resources.Load<Sprite>("Art/Playing Cards/" + suit + "/" + cardName);

        // Place card down in provided place.
        layCardPos.sprite = cardSprite;
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
    }
}
