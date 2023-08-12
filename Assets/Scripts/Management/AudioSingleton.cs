// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSingleton : MonoBehaviour
{
    // Singleton of AudioSingleton.
    private static AudioSingleton _instance;

    /// <summary> property <c>Instance</c> Provides get access to the _instance variable, allows objs to check for other AudioManagers presence. </summary>
    public static AudioSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioSingleton>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    // Called when the script instance is loaded.
    void Awake()
    {
        // Fills instance if empty, keeps obj through scene load.
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            // Destroies obj if its a duplicate.
            if (this != _instance) { Destroy(this.gameObject); }
        }
    }
}
