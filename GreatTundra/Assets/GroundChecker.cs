using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker
{
    private Collider2D objectCollider;
    private const string layerName = "Ground";
    private const float extraDepthCheck = 0.1f;
    private const float raycastRotation = 0f;
    private GroundChecker(){ }
    private GroundChecker(Collider2D objectToCheck)
    {
        objectCollider = objectToCheck;
    }

    private bool isObjectAboveGround()
    {
        RaycastHit2D groundCheck = Physics2D.BoxCast(calculateRaycastOrigin(), calculateRaycastSize(), 
            raycastRotation, Vector2.down, extraDepthCheck, getLayerMask());
        return groundCheck.collider != null;
    }

    private Vector2 calculateRaycastOrigin()
    {
        float yComponent = (objectCollider.bounds.center.y + objectCollider.bounds.min.y) / 2; //get halfway point for lower half only
        return new Vector3(objectCollider.bounds.center.x, yComponent, 0);
    }

    private Vector2 calculateRaycastSize()
    {
        Vector3 objectLowerHalf = new Vector3(objectCollider.bounds.size.x, objectCollider.bounds.size.y / 2, 0);
        return objectLowerHalf - new Vector3(0.2f, 0.1f, 0); //avoid walls and ceilings
    }

    private int getLayerMask()
    {
        return LayerMask.GetMask(layerName);
    }

    public static bool IsRigidbodyOnGround(Collider2D objectToCheck)
    {
        GroundChecker groundChecker = new GroundChecker(objectToCheck);
        return groundChecker.isObjectAboveGround();
    }
}
