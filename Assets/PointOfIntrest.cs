using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointOfIntrest : MonoBehaviour
{

    List<InputManager> playersInputManager = new List<InputManager>();

    [SerializeField] string MessageToDisplay = " ";

    [SerializeField] PopUpTextScript popUp;

    void OnTriggerEnter(Collider collider)
    {
        InputManager enteredInputManager;
        if (collider.transform.TryGetComponent<InputManager>(out enteredInputManager))
        {
            playersInputManager.Add(enteredInputManager);
        }

    }

    void OnTriggerStay(Collider collider)
    {
        foreach (var playerInputManager in playersInputManager)
        {
            if (playerInputManager.isInteract)
            {
                if (!popUp.isMessageDisplayed())
                {
                    popUp.DisplayContent(new StringBuilder(MessageToDisplay));
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        InputManager enteredInputManager;
        if (collider.transform.TryGetComponent<InputManager>(out enteredInputManager))
        {
            playersInputManager.Remove(enteredInputManager);
        }

        if (playersInputManager.Count <= 0)
        {
            if (popUp.isMessageDisplayed())
            {
                popUp.HideContent();
            }
        }

    }
}
