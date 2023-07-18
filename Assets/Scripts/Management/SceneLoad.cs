// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    /// <summary> method <c>LoadScene</c> Takes string name, uses SceneManagement to load scene by name. </summary>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary> method <c>QuitGame</c> Closes the window, only works in build. </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
