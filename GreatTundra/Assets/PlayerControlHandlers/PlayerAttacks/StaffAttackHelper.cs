using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffAttackHelper
{
    private Collider2D playerCollider;
    private LayerMask attackLayerMask;
    public StaffAttackHelper(GameObject playerObject)
    {
        playerCollider = playerObject.GetComponent<Collider2D>();
        attackLayerMask = LayerMask.GetMask(new string[] { "Puzzle", "Monster" });
    }

    private Vector2 calculateRaycastOrigin()
    {
        return new Vector3(playerCollider.bounds.max.x, playerCollider.bounds.center.y, 0);
    }

    private float calculateRaycastSize()
    {
        return playerCollider.bounds.size.y / 2;
    }

    private Vector2 calcuteSwingDirection()
    {
        return Vector2.right;
    }

    public void SwingStaff()
    {
        Collider2D[] allHits = Physics2D.OverlapCircleAll(calculateRaycastOrigin(), calculateRaycastSize(), attackLayerMask);
        foreach (Collider2D hit in allHits)
        {
            GameObject hitObject = hit.gameObject;
            tryHandleStaffPuzzleHit(hitObject);
            tryHandleMonsterHit(hitObject);
        }
    }

    private void tryHandleStaffPuzzleHit(GameObject hitObject)
    {
        if (hitObject.TryGetComponent(out StaffPuzzleBehaviour staffPuzzle))
        {
            staffPuzzle.SolvePuzzle();
        }
    }

    private void tryHandleMonsterHit(GameObject hitObject)
    {
        if (hitObject.TryGetComponent(out MonsterObject monster))
        {
            monster.HandlerPlayerStaffAttack(PlayerStats.Strength);
        }
    }
}
