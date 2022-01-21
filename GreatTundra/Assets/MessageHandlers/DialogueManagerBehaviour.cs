using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManagerBehaviour : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dialogueField;
    [SerializeField]
    private GamePauseBehaviour pauseBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        pauseBehaviour.PauseGame();
    }

    public void ResumeGame()
    {
        pauseBehaviour.ResumeGame();
    }

    public void DisplayMessage(string message)
    {
        dialogueField.text = message;
    }

    public void ClearMessage()
    {
        dialogueField.text = "";
    }
}
