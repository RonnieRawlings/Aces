// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
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
            endScreen.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!endScreen.activeInHierarchy)
        {
            EndCheck();
        }      
    }
}
