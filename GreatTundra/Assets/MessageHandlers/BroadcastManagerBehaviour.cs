using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BroadcastManagerBehaviour : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textField;
    public BroadcastBehaviour BroadcastObject { get; set; }

    new public void BroadcastMessage(string messageToDisplay)
    {
        textField.text = messageToDisplay;
    }

    public void ClearMessage()
    {
        textField.text = "";
    }
}
