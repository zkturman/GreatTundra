using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public static class PlayerStats
{
    private static float speed = 20f;
    public static float Speed 
    { 
        get => speed; 
        private set => speed = value;
    }

    public static void UpdateSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }
}
