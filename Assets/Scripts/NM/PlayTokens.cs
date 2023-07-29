using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTokens : MonoBehaviour
{
    [SerializeField] private NewMarketManagement nmManagement;
    [SerializeField] private GameObject horseParent;

    public int playerTokens = 10;

    /// <summary> method <c>PlayStartingToken</c> Sets componets & places middle token on player click. </summary>
    public void PlayMiddleToken()
    {
        // Removes token from player.
        playerTokens--;

        // Disables current outline/button + places middle token.
        transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<Button>().enabled = false;
        transform.parent.GetChild(4).GetChild(0).gameObject.SetActive(true);

        // Enables outlines/buttons for each horse.
        foreach (Transform child in horseParent.transform)
        {
            child.GetComponent<Outline>().enabled = true;
            child.GetComponent<Button>().enabled = true;
        }
    }

    /// <summary> method <c>PlaceHorseToken</c> Enables the horse token image on the selected horse, starts play. </summary>
    public void PlaceHorseToken(GameObject token)
    {
        // Removes token from player.
        playerTokens--;

        // Enables selected token.
        token.SetActive(true);

        // Disables all horse outlines/buttons;
        foreach (Transform child in horseParent.transform)
        {
            child.GetComponent<Outline>().enabled = false;
            child.GetComponent<Button>().enabled = false;
        }

        // Starting token placement has finished.
        nmManagement.StartTokensPlaced = true;
    }
}
 