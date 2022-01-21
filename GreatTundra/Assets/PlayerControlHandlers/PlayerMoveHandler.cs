using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveHandler : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private Collider2D playerCollider;
    [SerializeField]
    private float defaultRunSpeed = 20f;
    [SerializeField]
    private Camera sceneCamera;
    [SerializeField]
    private int platformLayerMask;
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
    }

    private void handlePlayerMovement()
    {
        float xVelocityComponent = Input.GetAxisRaw("Horizontal") * defaultRunSpeed;
        PlayerStats.UpdatePlayerDirection(xVelocityComponent);
        float yVelocityComponent = playerRigidBody.velocity.y;
        currentSpeed = new Vector2(xVelocityComponent, yVelocityComponent);
        playerRigidBody.velocity = currentSpeed;
    }

    private void FixedUpdate()
    {
        updateCamera();
    }

    private void updateCamera()
    {
        Vector3 newCameraPosition = new Vector3(transform.position.x, transform.position.y, sceneCamera.transform.position.z);
        Vector3 currentVelocity = new Vector3(currentSpeed.x, currentSpeed.y, 0);
        sceneCamera.transform.position = Vector3.SmoothDamp(sceneCamera.transform.position, newCameraPosition, ref currentVelocity, 0.1f);
    }
}
