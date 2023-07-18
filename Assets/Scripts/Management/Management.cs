// Author - Ronnie Rawlings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Management : MonoBehaviour
{
    /// <summary> method <c>ResetButtonSelected</c> Uses the event system to reset the currently selected button. </summary>
    public void ResetButtonSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
