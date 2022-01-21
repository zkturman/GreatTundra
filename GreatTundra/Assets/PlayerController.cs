using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, PausibleObject
{
    private bool playerIsPaused = false;
    public bool ObjectIsPaused 
    {
        get => playerIsPaused;
    }
    [SerializeField]
    private List<MonoBehaviour> playerControlHandlers = new List<MonoBehaviour>();
    [SerializeField]
    private Rigidbody2D rigidbody;
    private Vector2 defaultVelocity = Vector2.zero;

    public void SetObjectPauseFlag(bool pauseStatusFlag)
    {
        playerIsPaused = pauseStatusFlag;
        foreach (MonoBehaviour handler in playerControlHandlers)
        {
            handler.enabled = !playerIsPaused;
        }
        Vector2 velocityAtStatusChange = rigidbody.velocity;
        rigidbody.velocity = defaultVelocity;
        defaultVelocity = velocityAtStatusChange;
    }
}
