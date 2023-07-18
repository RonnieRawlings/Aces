// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnableEnd : MonoBehaviour
{
    [SerializeField] private GameObject endScreen, pile13;

    /// <summary> method <c>EndCheck</c> Checks to see if the game has concluded, enables endScreen if true. </summary>
    public void EndCheck()
    {
        int index = 0;
        foreach (Transform child in pile13.transform)
        {
            if (child.GetComponent<CardData>().cardNum == 13 && child.GetComponent<CardData>().isShown)
            {
                index++;
            }
        }

        if (index == 4)
        {
            // Shows end screen.
            endScreen.SetActive(true);

            // Calculates how close to winning the game was, resets shown data.
            endScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gameObject.GetComponent<ATCManagement>()
                .CalculateFinalPercentage() + "%";
            DeckData.amountShown = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if end should be shown if not enabled.
        if (!endScreen.activeInHierarchy)
        {
            EndCheck();
        }      
    }
}
