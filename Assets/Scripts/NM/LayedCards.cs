// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LayedCards : MonoBehaviour
{
    private Dictionary<Image, Sprite> childImages = new Dictionary<Image, Sprite>();
    [SerializeField] private string latestCard, latestPlayer, latestSuit;

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

        // Used for debugging static data.
        latestSuit = NMStaticData.latestSuit;
        latestPlayer = NMStaticData.latestPlayer;
        latestCard = NMStaticData.latestCard;

        // If player lays a horse card, collect tokens + flip horse.
        if (latestCard == "Ace of Spades" || latestCard == "King of Diamonds" || latestCard == "Jack of Hearts" || latestCard == "Queen of Clubs")
        {
            string horseSuit = latestCard.Split(' ')[2];
            CollectHorse(horseSuit);
        }
    }

    /// <summary> method <c>CollectHorse</c> Gives horse tokens to player IF horse card is layed. </summary>
    public void CollectHorse(string horseSuit)
    {
        Debug.Log(horseSuit);
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
