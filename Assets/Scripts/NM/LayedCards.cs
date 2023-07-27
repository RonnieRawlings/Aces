// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LayedCards : MonoBehaviour
{
    [SerializeField] private Transform canvas;
    private Dictionary<Image, Sprite> childImages = new Dictionary<Image, Sprite>();

    /// <summary> method <c>UpdateStaticCardData</c> Sets static card data from just layed card. </summary>
    public void UpdateStaticCardData()
    {
        // Create a list of keys to iterate over
        List<Image> keys = new List<Image>(childImages.Keys);

        // Check if any of the child Image components have changed their sprite
        for (int i = 0; i < keys.Count; i++)
        {
            Image image = keys[i];
            Sprite sprite = childImages[image];

            if (image.sprite != sprite)
            {
                // Update the stored sprite
                childImages[image] = image.sprite;

                // Pass the sprite name, suit, & player name to NMStaticData
                NMStaticData.latestCard = image.sprite.name;
                NMStaticData.latestPlayer = image.gameObject.name.Replace("P", "");
                NMStaticData.latestSuit = image.sprite.name.Split(' ')[2];
            }
        }

        // If player lays a horse card, collect tokens + flip horse.
        if (NMStaticData.latestCard == "Ace of Spades" || NMStaticData.latestCard == "King of Diamonds" || NMStaticData.latestCard == "Jack of Hearts" || NMStaticData.latestCard == "Queen of Clubs")
        {
            // Extracts card rank from latest card, collects horse.
            string horseRank = NMStaticData.latestCard.Split(' ')[0];
            CollectHorse(horseRank);
        }
    }

    /// <summary> method <c>CollectHorse</c> Gives horse tokens to player IF horse card is layed. </summary>
    public void CollectHorse(string horseRank)
    {
        // Flips the layed horse over.
        GameObject horse = canvas.Find("Horses").Find(horseRank).gameObject;
        if (horse.GetComponent<Image>().sprite.name == "Playing Card Back") { return; }
        horse.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/Playing Cards/Other/Playing Card Back");

        // Disables tokens on layed horse.
        GameObject horseTokens = canvas.Find("Tokens").Find("HorseTokens").Find((horse.transform.GetSiblingIndex() + 1)
            .ToString()).gameObject;

        // Iterates through tokens, counts layed.
        int totalTokens = 0;
        foreach (Transform child in horseTokens.transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                totalTokens++;
                child.gameObject.SetActive(false);
            }
        }

        // Gives tokens to player who layed the horse.
        if (NMStaticData.latestPlayer == "1")
        {
            // Adds the total horse tokens to the players total.
            PlayTokens playerTokens = horseTokens.transform.parent.parent.GetChild(0).GetComponent<PlayTokens>();
            playerTokens.playerTokens = playerTokens.playerTokens + totalTokens;
        }
        else
        {            
            // Adds the total horse tokens to the players total.
            CPUBehaviour cpuPlayer = canvas.Find("Player " + NMStaticData.latestPlayer.ToString()).GetComponent<CPUBehaviour>();
            cpuPlayer.PlayerTokens = cpuPlayer.PlayerTokens + totalTokens;
        }
    }

    void Start()
    {
        // Get all child Image components and store their current sprites
        foreach (Image image in GetComponentsInChildren<Image>())
        {
            childImages[image] = image.sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Updates static card data when new card is layed.
        UpdateStaticCardData(); 
    }
}
