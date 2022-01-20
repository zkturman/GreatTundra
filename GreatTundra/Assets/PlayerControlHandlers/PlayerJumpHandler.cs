using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpHandler : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private Collider2D playerCollider;
    [SerializeField]
    private float defaultJumpSpeed = 30f;
    [SerializeField]
    private float doubleJumpSpeedFactor = 0.7f;
    [SerializeField]
    private int jumpCounter = 0;
    [SerializeField]
    private float jumpCooldown = 0.25f;
    [SerializeField]
    private int platformLayerMask;
    private bool canJump = true;
    private bool isJumping = false;
    private Vector2 currentSpeed;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        platformLayerMask = 1 << platformLayerMask;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handlePlayerMovement();
        if (isJumping)
        {
            checkPlayerAboveGround();
        }
    }

    private void handlePlayerMovement()
    {
        float xVelocityComponent = playerRigidBody.velocity.x;
        float yVelocityComponent = playerRigidBody.velocity.y;

        if (canJump && determineIfJumpInput())
        {
            yVelocityComponent = handleJumpInput();
        }
        currentSpeed = new Vector2(xVelocityComponent, yVelocityComponent);
        playerRigidBody.velocity = currentSpeed;
    }

    private bool determineIfJumpInput()
    {
        bool playerHasJumped = false;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerHasJumped = true;
            StartCoroutine(isJumpingRoutine());
        }
        return playerHasJumped;
    }

    private IEnumerator isJumpingRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        isJumping = true;
    }

    private float handleJumpInput()
    {
        float jumpVelocity = playerRigidBody.velocity.y;
        if (jumpCounter == 0)
        {
            jumpVelocity = defaultJumpSpeed;
            jumpCounter++;
        }
        else if (jumpCounter == 1)
        {
            jumpVelocity = defaultJumpSpeed * doubleJumpSpeedFactor;
            jumpCounter++;
        }
        StartCoroutine(jumpCooldownRoutine());
        return jumpVelocity;
    }

    private IEnumerator jumpCooldownRoutine()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    private void checkPlayerAboveGround()
    {
        if (GroundChecker.IsRigidbodyOnGround(playerCollider))
        {
            jumpCounter = 0;
            isJumping = false;
        }
        //float extraDepthCheck = 0.1f;
        //RaycastHit2D groundCheck = Physics2D.BoxCast(calculateRaycastOrigin(), calculateRaycastSize(), 0f, Vector2.down, extraDepthCheck, platformLayerMask);
        //if (groundCheck.collider != null)
        //{
        //    jumpCounter = 0;
        //    isJumping = false;
        //}
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(calculateRaycastOrigin(), calculateRaycastSize());
    //}

    //private Vector2 calculateRaycastOrigin()
    //{
    //    float yComponent = (playerCollider.bounds.center.y + playerCollider.bounds.min.y) / 2; //get halfway point for lower half only
    //    Vector2 raycastOrigin = new Vector3(playerCollider.bounds.center.x, yComponent, 0);
    //    return raycastOrigin;
    //}

    //private Vector2 calculateRaycastSize()
    //{
    //    Vector2 raycastSize = new Vector3(playerCollider.bounds.size.x, playerCollider.bounds.size.y / 2, 0) - new Vector3(0.2f, 0.1f, 0);
    //    return raycastSize;
    //}

    private void FixedUpdate()
    {
        playerRigidBody.AddForce(new Vector3(0, -1.0f, 0) * playerRigidBody.mass * 9.8f);
    }



}
