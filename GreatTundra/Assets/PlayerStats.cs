using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public static class PlayerStats
{
    public enum Direction
    {
        Left,
        Right
    }
    private static Direction playerFacingDirection = Direction.Right;
    public static Direction PlayerFacingDirection 
    { 
        get => playerFacingDirection;
        private set => playerFacingDirection = value;
    }

    
    private static float speed = 20f;
    public static float Speed 
    { 
        get => speed; 
        private set => speed = value;
    }
    private static float strength = 1f;
    public static float Strength
    {
        get => strength;
        private set => strength = value;
    }

    public static void UpdateSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }

    public static void UpdatePlayerDirection(float horizontalAxisInput)
    {
        if (horizontalAxisInput < 0)
        {
            playerFacingDirection = Direction.Left;
        }
        else if (horizontalAxisInput > 0)
        {
            playerFacingDirection = Direction.Right;
        }
    }


    public static Vector3 GetProjectileDirection()
    {
        Vector2 returnValue;
        if (PlayerFacingDirection == Direction.Left)
        {
            returnValue = Vector2.left;
        }
        else
        {
            returnValue = Vector2.right;
        }
        return returnValue;
    }

}
