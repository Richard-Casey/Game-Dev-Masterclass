using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class PopUpTextScript : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI textMeshPro;

    bool isMessageActive = false;

    public bool isMessageDisplayed() => isMessageActive;

    public void SetContent(string input)
    {
        textMeshPro?.SetText(input);
    }

    public void DisplayContent(string input)
    {
        isMessageActive = true;
        gameObject.SetActive(true);
        SetContent(input);
    }

    public void HideContent()
    {
        isMessageActive = false;
        SetContent("");
        gameObject.SetActive(false);
    }

}
