using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBehaviour : MonoBehaviour
{
    [SerializeField]
    private string[] allDialogueFragments;
    [SerializeField]
    private DialogueManagerBehaviour dialogueManager;
    [SerializeField]
    private float textDelayInSeconds = 0.05f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayDialogueMessage()
    {
        dialogueManager.PauseGame();
        StartCoroutine(streamDialogueRoutine());
    }

    private IEnumerator streamDialogueRoutine()
    {
        foreach (string fragment in allDialogueFragments)
        {
            yield return streamFragmentRoutine(fragment);
            yield return waitForNextFragmentInputRoutine();
        }
        dialogueManager.ClearMessage();
        dialogueManager.ResumeGame();
    }

    private IEnumerator streamFragmentRoutine(string fragmentToDisplay)
    {
        StringBuilder messageBuilder = new StringBuilder();
        dialogueManager.ClearMessage();
        foreach(char c in fragmentToDisplay)
        {
            messageBuilder.Append(c);
            dialogueManager.DisplayMessage(messageBuilder.ToString());
            yield return new WaitForSeconds(textDelayInSeconds);
        }
    }

    private IEnumerator waitForNextFragmentInputRoutine()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
    }
}
