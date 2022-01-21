using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBroadcastBehaviour : BroadcastBehaviour
{
    private bool interactionComplete = false;
    [SerializeField]
    private string postInteractionMessage;
    [SerializeField]
    private DialogueBehaviour dialogueBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (interactionComplete)
        {
            broadcastMessage = postInteractionMessage;
        }
        base.OnCollisionEnter2D(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            if (!interactionComplete && this.Equals(broadcastManager.BroadcastObject))
            {
                tryHandleInteractionCommand();
            }
        }
    }

    private void tryHandleInteractionCommand()
    {
        if (!interactionComplete && Input.GetKeyDown(KeyCode.E))
        {
            broadcastManager.ClearMessage();
            dialogueBehaviour.PlayDialogueMessage();
            interactionComplete = true;
        }
    }
}
