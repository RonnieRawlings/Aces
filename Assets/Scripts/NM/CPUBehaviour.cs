// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUBehaviour : MonoBehaviour
{
    // Places where player can place their tokens.
    [SerializeField] private GameObject middleToken;
    [SerializeField] private List<GameObject> horseTokens;

    // Players starting tokens;
    [SerializeField] private int playerTokens = 10;

    /// <summary> method <c>StartingPlay</c> Starts the CPUs play, places starting tokens + chooses a horse. </summary>
    public void StartingPlay()
    {
        playerTokens = playerTokens - 2;
        middleToken.SetActive(true);
        horseTokens[Random.Range(0, 4)].SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartingPlay();
    }
}
