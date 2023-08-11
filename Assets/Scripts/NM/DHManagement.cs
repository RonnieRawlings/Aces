// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DHManagement : MonoBehaviour
{
    // Visuals + Interaction for player benefit.
    [SerializeField] private GameObject dummyHand;

    // Game Management script.
    [SerializeField] private NewMarketManagement nm;

    /// <summary> method <c>SwitchForDummyHand</c> Swaps the players current hand with the dummy hand. </summary>
    public void SwitchForDummyHand()
    {
        // Holds players hand before its changed.
        List<string> tempList = nm.PlayerOne;

        // Swaps hands over.
        nm.PlayerOne = nm.DummyHand;
        nm.DummyHand = tempList;
    }
}
