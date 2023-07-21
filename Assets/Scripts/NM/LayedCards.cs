// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayedCards : MonoBehaviour
{
    private Dictionary<Image, Sprite> childImages = new Dictionary<Image, Sprite>();
    [SerializeField] private string latestCard;

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

                // Pass the sprite name to NMStaticData.latestCard
                NMStaticData.latestCard = image.sprite.name;
            }
        }

        latestCard = NMStaticData.latestCard;
    }
}
