// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    /// <summary> method <c>CheckForKeyPress</c> Checks for ESCAPE press, if pressed change pause menu active value. </summary>
    public void CheckForKeyPress()
    {
        // Changes pause menu active value on key press.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isActive = !pauseMenu.activeInHierarchy;
            pauseMenu.SetActive(isActive);

            // Changes the timeScale, in accordance with pauseMenu activation.
            if (isActive)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForKeyPress();
    }
}
