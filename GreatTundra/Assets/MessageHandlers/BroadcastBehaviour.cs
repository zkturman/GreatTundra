using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastBehaviour : MonoBehaviour
{
    [SerializeField]
    protected string broadcastMessage;
    [SerializeField]
    protected BroadcastManagerBehaviour broadcastManager;
    protected bool isPlayerTouching = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            if (broadcastManager.BroadcastObject == null)
            {
                broadcastManager.BroadcastObject = this;
            }
            if (this.Equals(broadcastManager.BroadcastObject))
            {
                broadcastManager.BroadcastMessage(broadcastMessage);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.gameObject.TryGetComponent(out PlayerController player))
        //{
        //    if (!broadcastManager.IsBroadcasting)
        //    {
        //        broadcastManager.BroadcastMessage(broadcastMessage);
        //    }
        //}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            if (this.Equals(broadcastManager.BroadcastObject))
            {
                broadcastManager.ClearMessage();
                broadcastManager.BroadcastObject = null;
            }
        }
    }
}
